using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string readPatch = "";
                Console.WriteLine("Укажите пусть XML файла ->>->>->>->> ");
                readPatch = Console.ReadLine();
                XmlDocument xDoc = new XmlDocument();
                if (readPatch == "")
                {
                    Console.WriteLine("Производиться считывание будет в папке Debug/bin/ файл users.xml");
                    xDoc.Load("users.xml");
                }
                else
                    xDoc.Load(readPatch);


                XmlElement xRoot = xDoc.DocumentElement;

                string writePatch = "";
                Console.WriteLine("Укажите пусть JSON файла для сохранения (в виде C:\\qwerty\\123.json)->>->>->>->> ");
                writePatch = Console.ReadLine();
                if (writePatch == "")
                {
                    Console.WriteLine("Сохранение будет производиться в файл, в папке C:\\users.json файл users.json");
                    writePatch = @"C:\users.json";
                    using (StreamWriter sw = new StreamWriter(writePatch, false, System.Text.Encoding.Default))
                    {
                        NodeReturn(xRoot, sw);
                    }
                }
                else
                {

                    using (StreamWriter sw = new StreamWriter(writePatch, false, System.Text.Encoding.Default))
                    {
                        NodeReturn(xRoot, sw);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: " + ex);
            }
            finally
            {
                Main(args);
            }

          

            Console.Read();
        }

        static void NodeReturn(XmlNode xRoot, StreamWriter sw)
        {
            try
            {
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
              
                foreach (XmlNode xnode in xRoot)
                {

                    
                    if (xnode.NextSibling != null)
                    {
                        if (xnode.PreviousSibling != null && xnode.PreviousSibling.Name == xnode.NextSibling.Name)
                        {
                            sw.WriteLine("},");
                            sw.WriteLine(xnode.Name + "{");
                            Console.WriteLine("},");
                            Console.WriteLine(xnode.Name + "{");
                            if (xnode.Attributes.Count > 0)
                            {
                                foreach (XmlAttribute atr in xnode.Attributes)
                                {
                                    Console.WriteLine(atr.Name + " : " + atr.Value + ", ");
                                    sw.WriteLine(atr.Name + " : " + atr.Value + ", ");
                                }
                            }

                        }
                        else
                        if (xnode.NextSibling.Name == xnode.Name)
                        {
                            flag = true;
                            Console.Write("'" + xnode.Name + "' : [");
                            sw.Write("'" + xnode.Name + "' : [");
                            Console.WriteLine("{");
                            sw.WriteLine("{");
                            if (xnode.Attributes.Count > 0)
                            {
                                foreach (XmlAttribute atr in xnode.Attributes)
                                {
                                    Console.WriteLine(atr.Name + " : " + atr.Value + ", ");
                                    sw.WriteLine(atr.Name + " : " + atr.Value + ", ");
                                }
                            }


                        }
                        else
                        {
                            if (xnode.FirstChild != xnode.LastChild && xnode.NodeType == XmlNodeType.Element)
                            {
                                if (xnode.HasChildNodes == true)
                                {
                                    flag2 = true;
                                    Console.WriteLine("'" + xnode.Name + "' : {");
                                    sw.WriteLine("'" + xnode.Name + "' : {");
                                }
                            }
                            else
                            {
                               
                                if (xnode.FirstChild != null && xnode.LastChild != null)
                                    if (xnode.FirstChild.NodeType != XmlNodeType.Text && xnode.LastChild.NodeType != XmlNodeType.Text)
                                    {
                                        flag3 = true;
                                        Console.WriteLine(xnode.Name + " : {");
                                        sw.WriteLine(xnode.Name + " : {");
                                    }
                                    else
                                    {
                                        Console.WriteLine(xnode.Name + " : " + xnode.InnerText + ",");
                                        sw.WriteLine(xnode.Name + " : " + xnode.InnerText + ",");
                                    }
                             

                            }
                        }
                    }
                    else
                    {
                        if (xnode.PreviousSibling != null)
                        {
                            if (xnode.PreviousSibling.Name == xnode.Name)
                            {
                                Console.WriteLine("},{");
                                sw.WriteLine("},{");
                                if (xnode.Attributes.Count > 0)
                                {
                                    foreach (XmlAttribute atr in xnode.Attributes)
                                    {
                                        Console.WriteLine(atr.Name + " : " + atr.Value + ", ");
                                        sw.WriteLine(atr.Name + " : " + atr.Value + ", ");
                                    }
                                }

                                Console.WriteLine(" }");
                                sw.WriteLine(" }");
                            }
                            else
                            {
                                if (xnode.FirstChild != xnode.LastChild && xnode.NodeType == XmlNodeType.Element)
                                {
                                    if (xnode.HasChildNodes == true)
                                    {
                                        flag2 = true;
                                        Console.WriteLine("'" + xnode.Name + "' : {");
                                        sw.WriteLine("'" + xnode.Name + "' : {");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(xnode.Name + " : ");
                                    sw.WriteLine(xnode.Name + " : ");
                                }
                            }
                        }
                        else
                        {
                            if (xnode.FirstChild != xnode.LastChild && xnode.NodeType == XmlNodeType.Element)
                            {
                                if (xnode.HasChildNodes == true)
                                {
                                    flag2 = true;
                                    Console.WriteLine("'" + xnode.Name + "' : {");
                                    sw.WriteLine("'" + xnode.Name + "' : {");

                                    if (xnode.Attributes.Count > 0)
                                    {
                                        foreach (XmlAttribute atr in xnode.Attributes)
                                        {
                                            Console.WriteLine(atr.Name + " : " + atr.Value + ", ");
                                            sw.WriteLine(atr.Name + " : " + atr.Value + ", ");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(xnode.Name + " : " + xnode.InnerText);
                                sw.WriteLine(xnode.Name + " : " + xnode.InnerText);
                            }
                        }
                    }




                  

                    if (xnode.FirstChild != null)
                        if (xnode.FirstChild.NodeType != XmlNodeType.Text)
                            NodeReturn(xnode, sw);
                    if (xnode.NextSibling == null)
                        if (xnode.PreviousSibling != null)
                            if (xnode.PreviousSibling.Name == xnode.Name)
                                if (flag == true)
                                {
                                    Console.WriteLine("]");
                                    sw.WriteLine("]");
                                }

                    if (flag2 == true)
                    {
                        Console.WriteLine("}");
                        sw.WriteLine("}");
                    }

                    if (flag3 == true)
                    {
                        Console.WriteLine("} ,");
                        sw.WriteLine("} ,");
                    }

                 
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR в методе NodeReturn(): код(): " + ex);
            }
           
        }
    }
}
