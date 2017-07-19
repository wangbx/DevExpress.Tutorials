namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.Threading;
    using System.Windows.Forms;

    public class ExampleEvaluatorByTimer : IDisposable
    {
        private bool compileComplete;
        public System.Windows.Forms.Timer compileExampleTimer;
        public const int CompileTimeIntervalInMilliseconds = 0x514;
        private ExampleCodeEvaluator exampleCodeEvaluator;
        private readonly LeakSafeCompileEventRouter leakSafeCompileEventRouter;

        public event OnAfterCompileEventHandler OnAfterCompile;

        public event EventHandler OnBeforeCompile;

        public event CodeEvaluationEventHandler QueryEvaluate;

        public ExampleEvaluatorByTimer() : this(true)
        {
        }

        public ExampleEvaluatorByTimer(bool enableTimer)
        {
            this.compileComplete = true;
            this.leakSafeCompileEventRouter = new LeakSafeCompileEventRouter(this);
            if (enableTimer)
            {
                this.compileExampleTimer = new System.Windows.Forms.Timer();
                this.compileExampleTimer.Interval = 0x514;
                this.compileExampleTimer.Tick += new EventHandler(this.leakSafeCompileEventRouter.OnCompileExampleTimerTick);
                this.compileExampleTimer.Enabled = true;
            }
        }

        public void CompileExample()
        {
            if (this.compileComplete)
            {
                CodeEvaluationEventArgs args = this.RaiseQueryEvaluate();
                if (args.Result)
                {
                    this.ForceCompile(args);
                }
            }
        }

        private void CompileExampleAndShowPrintPreview(CodeEvaluationEventArgs args)
        {
            bool result = false;
            try
            {
                this.RaiseOnBeforeCompile();
                result = this.Evaluate(args);
            }
            finally
            {
                this.RaiseOnAfterCompile(result);
            }
        }

        public void Dispose()
        {
            if (this.compileExampleTimer != null)
            {
                this.compileExampleTimer.Enabled = false;
                if (this.leakSafeCompileEventRouter != null)
                {
                    this.compileExampleTimer.Tick -= new EventHandler(this.leakSafeCompileEventRouter.OnCompileExampleTimerTick);
                }
                this.compileExampleTimer.Dispose();
                this.compileExampleTimer = null;
            }
        }

        public bool Evaluate(CodeEvaluationEventArgs args)
        {
            return this.GetExampleCodeEvaluator(args.Language).ExecuteCodeAndGenerateLayout(args);
        }

        public void ForceCompile(CodeEvaluationEventArgs args)
        {
            this.compileComplete = false;
            if (args.CodeExample != null)
            {
                this.CompileExampleAndShowPrintPreview(args);
            }
            this.compileComplete = true;
        }

        protected ExampleCodeEvaluator GetExampleCodeEvaluator(ExampleLanguage language)
        {
            if (this.exampleCodeEvaluator == null)
            {
                this.exampleCodeEvaluator = new ExampleCodeEvaluator(language);
            }
            return this.exampleCodeEvaluator;
        }

        private void RaiseOnAfterCompile(bool result)
        {
            if (this.OnAfterCompile != null)
            {
                OnAfterCompileEventArgs e = new OnAfterCompileEventArgs {
                    Result = result
                };
                this.OnAfterCompile(this, e);
            }
        }

        private void RaiseOnBeforeCompile()
        {
            if (this.OnBeforeCompile != null)
            {
                this.OnBeforeCompile(this, new EventArgs());
            }
        }

        protected internal virtual CodeEvaluationEventArgs RaiseQueryEvaluate()
        {
            if (this.QueryEvaluate != null)
            {
                CodeEvaluationEventArgs e = new CodeEvaluationEventArgs();
                this.QueryEvaluate(this, e);
                return e;
            }
            return null;
        }
    }
}

