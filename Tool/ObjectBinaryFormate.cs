using System;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
//此处需要进相关的网站下载相应的开源插件
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip;


namespace Tool
{
     public class ObjectBinaryFormate
    {
        public ObjectBinaryFormate()
        {

        }

        /**//**********************************************************************************************
         * 方法名称：ChangeObjectToBytes
         * 功能说明：把数据对象序列化为字节型数组
         * 输 入 值：数据对象
         * 输 出 值：无
         * 返 回 值：字节数组
         * 其它说明：无
        **********************************************************************************************/
        public static byte[] ChangeObjectToBytes(object objValue)
        {
            byte[] dataValue = null;
            try
            {
                //序列化
                BinaryFormatter formate = new BinaryFormatter();
                //内存文件流对象
                MemoryStream smsStream = new MemoryStream();
                formate.Serialize(smsStream, objValue);
                dataValue = smsStream.ToArray();
                smsStream.Close();
            }
            catch(Exception e)
            {
                System.Console.WriteLine("序列化失败！"+e.Message);
            }

            //返回压缩后的数据
            return CompressByteData(dataValue);
        }

        
        /**//**********************************************************************************************
         * 方法名称：ChangeBytesToObject
         * 功能说明：把字节型数组反序列为数据对象
         * 输 入 值：字节数组
         * 输 出 值：无
         * 返 回 值：数据对象
         * 其它说明：无
        **********************************************************************************************/
        public static object ChangeBytesToObject(byte[] dataValue)
        {
            object objValue = null;
            try
            {
                //解压缩数据
                byte[] resultValue = DecompressByteData(dataValue);

                //反序列化
                BinaryFormatter formate = new BinaryFormatter();
                //内存文件流对象
                MemoryStream smsStream = new MemoryStream();
                smsStream.Write(resultValue, 0, resultValue.Length);
                //指针归零
                smsStream.Seek(0,  SeekOrigin.Begin);  
                objValue = (object)formate.Deserialize(smsStream);
                smsStream.Close();

            }
            catch(Exception e)
            {
                System.Console.WriteLine("反序列化失败！"+e.Message);
            }

            //返回对象
            return objValue;
        }


        /**//**********************************************************************************************
         * 方法名称：CompressByteData
         * 功能说明：数据压缩
         * 输 入 值：源字节数据
         * 输 出 值：无
         * 返 回 值：压缩后的字节数据
         * 其它说明：无
        **********************************************************************************************/
        private static byte[] CompressByteData(Byte[] dataValue)
        {
            byte[] resultValue =  null;
            try
            {
                //压缩数据
                Deflater compressFile = new Deflater(Deflater.BEST_COMPRESSION);
                compressFile.SetInput(dataValue);
                compressFile.Finish();
                //内存文件流对象
                MemoryStream smsStream = new MemoryStream();
                byte[] bufData = new byte[1024];
                while (!compressFile.IsFinished) 
                {
                    int bufLength = compressFile.Deflate(bufData);
                    smsStream.Write(bufData, 0, bufLength);
                }
                resultValue = smsStream.ToArray();
                smsStream.Close();

            }
            catch(Exception e)
            {
                System.Console.WriteLine("压缩数据失败！"+e.Message);
            }

            //返回压缩后的数据
            return resultValue;
        }


        /**//**********************************************************************************************
         * 方法名称：DecompressByteData
         * 功能说明：解压缩数据
         * 输 入 值：源字节数据
         * 输 出 值：无
         * 返 回 值：解压后数据
         * 其它说明：无
        **********************************************************************************************/
        private static byte[] DecompressByteData(byte[] dataValue)
        {
            byte[] resultValue = null;
            try
            {
                //解压缩数据
                Inflater decompressFile = new Inflater();
                decompressFile.SetInput(dataValue);
                //内存文件流对象
                MemoryStream smsStream = new MemoryStream();
                byte[] bufData = new byte[1024];
                while (!decompressFile.IsFinished) 
               {
                    int bufLength = decompressFile.Inflate(bufData);
                    smsStream.Write(bufData, 0, bufLength);
                }
                resultValue = smsStream.ToArray();
                smsStream.Close();
            }
            catch(Exception e)
            {
                System.Console.WriteLine("解压缩数据失败！"+e.Message);
            }

            //返回数据
            return resultValue;
        }


        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="dirPath">压缩文件夹的路径</param>
        /// <param name="fileName">生成的zip文件路径</param>
        /// <param name="level">压缩级别 0 - 9 0是存储级别 9是最大压缩</param>
        /// <param name="bufferSize">读取文件的缓冲区大小</param>
        public static void CompressDirectory(string dirPath,string fileName,int level,int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            using (ZipOutputStream s = new ZipOutputStream(File.Create(fileName)))
            {
                s.SetLevel(level);
                CompressDirectory(dirPath, dirPath, s, buffer);
                s.Finish();
                s.Close();
            }
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="root">压缩文件夹路径</param>
        /// <param name="path">压缩文件夹内当前要压缩的文件夹路径</param>
        /// <param name="s"></param>
        /// <param name="buffer">读取文件的缓冲区大小</param>
        private static void CompressDirectory(string root, string path, ZipOutputStream s, byte[] buffer)
        {
            //root = root.TrimEnd('/') + "//";
            root = root.TrimEnd('/');
            string[] fileNames = Directory.GetFiles(path);
            string[] dirNames = Directory.GetDirectories(path);
            string relativePath = path.Replace(root, "");
            if (relativePath != "")
            {
                relativePath = relativePath.Replace("//", "/") + "/";
            }
            int sourceBytes;
            foreach (string file in fileNames)
            {

                ZipEntry entry = new ZipEntry(relativePath+Path.GetFileName(file));
                entry.DateTime = DateTime.Now;
                s.PutNextEntry(entry);
                using (FileStream fs = File.OpenRead(file))
                {
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        s.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }

            foreach (string dirName in dirNames)
            {
                string relativeDirPath = dirName.Replace(root, "");
                ZipEntry entry = new ZipEntry(relativeDirPath.Replace("//", "/") + "/");
                s.PutNextEntry(entry);
                CompressDirectory(root, dirName, s, buffer);
            }
        }

        /// <summary>
        /// 解压缩zip文件
        /// </summary>
        /// <param name="zipFilePath">解压的zip文件路径</param>
        /// <param name="extractPath">解压到的文件夹路径</param>
        /// <param name="bufferSize">读取文件的缓冲区大小</param>
        public static void Extract(string zipFilePath, string extractPath, int bufferSize)
        {
            extractPath = extractPath.TrimEnd('/') + "//";
            byte[] data = new byte[bufferSize];
            int size;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                ZipEntry entry;
                while ((entry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(entry.Name);
                    string fileName = Path.GetFileName(entry.Name);

                    //先创建目录
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(extractPath + directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(extractPath + entry.Name.Replace("/", "//")))
                        {
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }


     
     }
}


