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
                        //sw.WriteLine("");
                        //sw.Write(4.5);
                    }
                }
                else
                {

                    using (StreamWriter sw = new StreamWriter(writePatch, false, System.Text.Encoding.Default))
                    {
                        NodeReturn(xRoot, sw);
                        //sw.WriteLine("");
                        //sw.Write(4.5);
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

            // получим корневой элемент
            //XmlElement xRoot = xDoc.DocumentElement;
            // обход всех узлов в корневом элементе
            /*     foreach (XmlNode xnode in xRoot)
                 {
                     Console.WriteLine("'" + xnode.Name + "' : {" );
                     // получаем атрибут name
                     if (xnode.Attributes.Count > 0)
                     {
                         XmlNode attr = xnode.Attributes.GetNamedItem("name");
                         if (attr != null)
                             Console.WriteLine(attr.Value);
                     }
                     // обходим все дочерние узлы элемента user
                     foreach (XmlNode childnode in xnode.ChildNodes)
                     {
                         // если узел - company
                         if (childnode.Name == "company")
                         {
                             Console.WriteLine("Компания: {0}", childnode.InnerText);
                         }
                         // если узел age
                         if (childnode.Name == "age")
                         {
                             Console.WriteLine("Возраст: {0}", childnode.InnerText);
                         }
                     }
                     Console.WriteLine();
                 }*/
          //  string writePath = @"C:\ath.json";
            


            //NodeReturn(xRoot);

            Console.Read();
        }

        static void NodeReturn(XmlNode xRoot, StreamWriter sw)
        {
            try
            {
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                // if(xRoot.LocalName != "#text")
                foreach (XmlNode xnode in xRoot)
                {

                    //Console.WriteLine("'" + xnode.Name + "' : {" );
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
                            //  Console.WriteLine("[");
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
                                //  if(xnode.FirstChild!= null && xnode.LastChild != null)
                                // {
                                //Console.WriteLine(xnode.Name + " : {");
                                //NodeReturn(xnode);
                                // }
                                // else
                                // {
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
                                // }

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




                    /*   else
                       {
                           if(xnode.NextSibling.Name == xnode.Name)
                           Console.WriteLine("'" + xnode.Name + "' : [ \n {");
                           else
                           {

                           }
                       }*/

                    /*if (xnode.FirstChild != xnode.LastChild && xnode.NodeType == XmlNodeType.Element)
                    {
                        if (xnode.HasChildNodes == true)
                            Console.WriteLine("'" + xnode.Name + "' : {");
                    }
                    else
                    {
                        Console.WriteLine(xnode.Name + " : ");
                    }*/
                    //^^^ РАБОЧИЙ КОД

                    /*  if (xRoot.FirstChild == xRoot.LastChild && xnode.HasChildNodes == true)
                      {
                          Console.WriteLine("[");
                      }*/

                    if (xnode.FirstChild != null)
                        if (/*xnode.FirstChild != xnode.LastChild &&*/ xnode.FirstChild.NodeType != XmlNodeType.Text)
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

                    /*    if (xRoot.FirstChild == xRoot.LastChild && xnode.HasChildNodes == true)
                        {
                            Console.WriteLine("]");
                        }*/
                }

                //NodeReturn(xnode.ChildNodes);
                // Console.WriteLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR в методе NodeReturn(): код(): " + ex);
            }
           
        }
    }
}


// получаем атрибут name
/*     if (xnode.FirstChild != null)
         if (xnode.Attributes.Count > 0)
     {
         XmlNode attr = xnode.Attributes.GetNamedItem("name");
         if (attr != null)
             Console.WriteLine(attr.Value);
     }*/
// обходим все дочерние узлы элемента user
//   if (xnode.FirstChild != null)
/*       foreach (XmlNode childnode in xRoot.ChildNodes)
       {
       Console.WriteLine(childnode.Name+ " : " + childnode.InnerText);
       NodeReturn(childnode);*
       // если узел - company
       /*   if (childnode.Name == "company")
          {
              Console.WriteLine("Компания: {0}", childnode.InnerText);
          }
          // если узел age
          if (childnode.Name == "age")
          {
              Console.WriteLine("Возраст: {0}", childnode.InnerText);
          }*/
