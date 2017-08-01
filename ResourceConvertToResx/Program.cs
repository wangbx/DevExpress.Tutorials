using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ResourceConvertToResx
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = GetProjectFileExists();
            if (!flag)
            {
                Console.WriteLine("项目文件不存在");
                goto skip;
            }
            GetXMLProjectFile();
            GetResourceFile();
            projectXmlFile.Save(projectFilePath);
            Console.WriteLine("执行结束");

            skip:
            Console.Read();
        }

        private static string filePath = @"D:\Work\Test\DevExpress.v16.1\DevExpress.Tutorials.v16.1\";
        static string projectFilePath = null;
        static string projectFileName = null;
        static XmlDocument projectXmlFile = null;

        /// <summary>
        /// 判断项目文件是否存在
        /// </summary>
        /// <returns></returns>
        private static bool GetProjectFileExists()
        {
            bool exist = false;
            if (!Directory.Exists(filePath))
            {
                Console.WriteLine("指定目录不存在");
                return exist;
            }
            DirectoryInfo currentDir = new DirectoryInfo(filePath);
            projectFileName = currentDir.Name + ".csproj";
            projectFilePath = Path.Combine(filePath, projectFileName);
            return File.Exists(projectFilePath);
        }
        /// <summary>
        /// 加载项目文件
        /// </summary>
        private static void GetXMLProjectFile()
        {
            projectXmlFile = new XmlDocument();
            try
            {
                projectXmlFile.Load(projectFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("加载项目文件：{0} 失败", projectFileName);
                Console.WriteLine(ex.Message);
            }
        }

        private static void GetResourceFile()
        {
            if (projectXmlFile == null)
            {
                Console.WriteLine("项目文件异常");
                return;
            }
            DirectoryInfo currentDir = new DirectoryInfo(filePath);
            Dictionary<string, List<string>> projectFileAddInfo = new Dictionary<string, List<string>>();

            FileInfo[] fileArray = currentDir.GetFiles("*.resources", SearchOption.AllDirectories);
            foreach (FileInfo file in fileArray)
            {
                if (file.FullName.IndexOf("bin\\Debug") > -1)
                    continue;
                IResourceReader reader = new ResourceReader(file.FullName);
                if (reader == null)
                    continue;

                string tempResourceFileName = file.Name.Replace(file.Extension, "");
                string[] tempArray = tempResourceFileName.Split('.');
                if (tempArray.Length < 1)
                {
                    Console.WriteLine("文件名称异常：" + tempResourceFileName);
                    continue;
                }

                string currentFilePath = null;
                string relativePath = "";
                string resourceFileName = null;
                string dependFileName = null;
                string tempFileName = tempResourceFileName;

                int i = 0;
                if (tempArray.Length > 1)
                {
                    bool dependFileExist = false;
                    while (i < tempArray.Length)
                    {
                        tempFileName = tempFileName.Replace(tempArray[i] + ".", "");
                        dependFileName = tempFileName + ".cs";
                        relativePath = Path.Combine(relativePath, tempArray[i]);
                        currentFilePath = Path.Combine(filePath, relativePath, dependFileName);

                        i++;
                        if (!File.Exists(currentFilePath))
                            continue;
                        dependFileExist = true;
                        resourceFileName = tempFileName + ".resx";
                        currentFilePath = currentFilePath.Replace(dependFileName, "");
                        break;
                    }
                    if (!dependFileExist)
                    {
                        //Console.WriteLine("资源文件依赖代码文件不存在：" + tempResourceFileName);
                        //continue;
                        resourceFileName = tempResourceFileName + ".resx";
                        currentFilePath = filePath;
                        relativePath = filePath;
                        dependFileName = "";
                    }
                }
                else
                {
                    dependFileName = tempArray[0] + ".cs";
                    relativePath = "";
                    currentFilePath = Path.Combine(filePath, dependFileName);
                    if (!File.Exists(currentFilePath))
                    {
                        Console.WriteLine("资源文件依赖代码文件不存在：" + tempResourceFileName);
                        continue;
                    }
                    resourceFileName = tempArray[0] + ".resx";
                    currentFilePath = filePath;
                }

                if (!Directory.Exists(currentFilePath))
                {
                    Console.WriteLine("目录：{0} 不存在", currentFilePath);
                    continue;
                }
                if (File.Exists(currentFilePath + "\\" + resourceFileName))
                {
                    File.Delete(currentFilePath + "\\" + resourceFileName);
                }

                ResXResourceWriter writer = new ResXResourceWriter(currentFilePath + "\\" + resourceFileName);
                IDictionaryEnumerator en = reader.GetEnumerator();
                while (en.MoveNext())
                {
                    if (en.Value != null && en.Value.GetType().Name == "PinnedBufferMemoryStream")
                    {
                        UnmanagedMemoryStream us = en.Value as UnmanagedMemoryStream;
                        byte[] usArray = new byte [(int)us.Length];
                        us.Read(usArray,0, usArray.Length);

                        MemoryStream ms = new MemoryStream(usArray, true);
                        writer.AddMetadata(en.Key.ToString(), ms);

                        //FileStream fs = new FileStream("D:\\test.xml" , FileMode.CreateNew);
                        //fs.Write(usArray,0, usArray.Length);
                        //fs.Dispose();
                        //string content = ASCIIEncoding.ASCII.GetString(usArray,0,usArray.Length);
                        //string content2 = Encoding.UTF8.GetString(usArray);
                    }
                    else
                        writer.AddMetadata(en.Key.ToString(), en.Value);
                }
                writer.Close();
                writer.Dispose();
                reader.Dispose();

                //把资源文件信息写入项目文件中
                string perfixInfo = "Resources\\";

                relativePath = Path.Combine(relativePath, resourceFileName);
                if (projectFileAddInfo.ContainsKey(perfixInfo + file.Name))
                    continue;

                projectFileAddInfo.Add(perfixInfo + file.Name, new List<string>() { relativePath, dependFileName });
                file.Delete();
            }
            WriteResourceInfoToProjectFile(projectFileAddInfo);
        }
        private static void WriteResourceInfoToProjectFile(Dictionary<string, List<string>> projectFileAddInfo)
        {
            if (projectXmlFile == null)
            {
                Console.WriteLine("项目文件异常");
                return;
            }
            if (projectFileAddInfo.Count < 1)
            {
                Console.WriteLine("无待处理资源文件");
                return;
            }
            //项目文档中使用了命名空间 ，使用XPath时 只有包含命名空间信息才可查询到节点信息
            string xPath = @"/ns:Project/ns:ItemGroup";
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(projectXmlFile.NameTable);
            nsmgr.AddNamespace("ns", projectXmlFile.DocumentElement.NamespaceURI);
            ////XmlNode parentNode2 = projectXmlFile.DocumentElement.SelectSingleNode(xPath, nsmgr);
            XmlNodeList parentNode = projectXmlFile.DocumentElement.SelectNodes(xPath, nsmgr);
            xPath = @"/ns:Project/ns:ItemGroup/ns:EmbeddedResource";
            XmlNodeList ResourceList = projectXmlFile.DocumentElement.SelectNodes(xPath, nsmgr);

            //XmlNodeList parentNode = projectXmlFile.GetElementsByTagName("ItemGroup");
            if (parentNode == null || parentNode.Count  < 1)
            {
                Console.WriteLine("未查找到ItemGroup节点");
                return;
            }
            
            bool tempFlag = false;

            foreach (var Item in projectFileAddInfo)
            {
                tempFlag = false;
                List<XmlNode> existList = new List<XmlNode>();
                if(ResourceList != null && ResourceList.Count > 0)
                {
                    foreach (XmlNode ChildNode in ResourceList)
                    {
                        if (ChildNode.Name != "EmbeddedResource")
                            continue;
                        if (ChildNode.Attributes["Include"] == null)
                            continue;
                        if (ChildNode.Attributes["Include"].Value == Item.Key || ChildNode.Attributes["Include"].Value == Item.Value[0])
                        {
                            existList.Add(ChildNode);
                            tempFlag = true;
                        }
                    }
                }
                if (tempFlag)
                {
                    foreach(XmlNode tempNode in existList)
                        tempNode.ParentNode.RemoveChild(tempNode);
                }
                XmlNode resourcenNode = projectXmlFile.CreateElement("EmbeddedResource", projectXmlFile.DocumentElement.NamespaceURI);
                XmlAttribute resourcenNodeAttr = projectXmlFile.CreateAttribute("Include");
                resourcenNodeAttr.Value = Item.Value[0];
                resourcenNode.Attributes.Append(resourcenNodeAttr);

                if (!string.IsNullOrWhiteSpace(Item.Value[1]))
                {
                    XmlNode dependNode = projectXmlFile.CreateElement("DependentUpon", projectXmlFile.DocumentElement.NamespaceURI);
                    XmlNode dependNodeText = projectXmlFile.CreateTextNode(Item.Value[1]);
                    //dependNodeText.Value = Item.Value[1];
                    dependNode.AppendChild(dependNodeText);
                    resourcenNode.AppendChild(dependNode);
                }
                parentNode[0].AppendChild(resourcenNode);
            }
        }
    }
}