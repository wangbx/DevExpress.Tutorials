namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;

    public class LeakSafeCompileEventRouter
    {
        private readonly WeakReference weakControlRef;

        public LeakSafeCompileEventRouter(ExampleEvaluatorByTimer module)
        {
            this.weakControlRef = new WeakReference(module);
        }

        public void OnCompileExampleTimerTick(object sender, EventArgs e)
        {
            ExampleEvaluatorByTimer target = (ExampleEvaluatorByTimer) this.weakControlRef.Target;
            if (target != null)
            {
                target.CompileExample();
            }
        }
    }
}

