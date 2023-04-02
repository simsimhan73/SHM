using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using SHM.Model;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using System.Data.Common;
using System.Windows;
using SHM.View;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace SHM.Core
{
    internal class SeatCore
    {
        public static void Suffle<T>(ref ObservableCollection<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // 리스트를 2차원 리스트로 변환
        public static List<List<string>> STOL(ObservableCollection<ChairModel> list, int row, int column)
        {
            List<List<string>> res = new List<List<string>>();
            int k = 0;
            for (int i = 0; i < row; i++)
            {
                res.Add(new List<string>());
                for (int j = 0; j < column; j++)
                {
                    res[i].Add(list[k].Name);
                    k++;
                }
            }
            return res;
        }

        public static void CreatePDF(List<List<string>> list, int column, int row)
        {
            string path = "자리배치.pdf";
            iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER);

            FileStream file;

            if (!File.Exists(path)) { file = File.Create(AppDomain.CurrentDomain.BaseDirectory + "\\" + path); file.Close(); };

            // PdfWriter 설정
            PdfWriter wr = PdfWriter.GetInstance(doc, new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\" + path, FileMode.Create));

            int euckrCodePage = 51949;  // euc-kr 코드 번호
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            System.Text.Encoding euckr = System.Text.Encoding.GetEncoding(euckrCodePage);

            try
            {

                // 문서 열기
                doc.Open();

                // 한글 설정
                string ttf = System.IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), @"Fonts\MALGUNSL.TTF");
                BaseFont baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                var font = new iTextSharp.text.Font(baseFont, 12F);

                var table = new PdfPTable(column) { WidthPercentage = 100 };



                for (int i = 0; i < row; i++)
                    for (int j = 0; j < column; j++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(list[i][j], font));
                        cell.FixedHeight = doc.PageSize.Height / 2 / row;
                        cell.VerticalAlignment = 1;
                        cell.HorizontalAlignment = 1;
                        cell.Rotation = 180;
                        table.AddCell(cell);
                    }

                doc.Add(table);
            }
            catch (Exception ex)
            {
                // 오류시 메시지 박스 보여주기
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                doc.Close();
                wr.Close();
            }
        }

        public static void Save(ObservableCollection<ChairModel> list, int hei, int wid)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\SaveFile.json";

            if (!File.Exists(path))
            {
                using (File.Create(path)) MessageBox.Show("파일 생성 성공");
            }

            JObject db = new JObject();

            db.Add("Data", JArray.FromObject(STOL(list, hei, wid)));

            File.WriteAllText(path, db.ToString());
        }

        public static void Load(ref ObservableCollection<ChairModel> list, int hei, int wid)
        {
            list.Clear();

            if (wid == 0 || hei == 0) { MessageBox.Show("가로나 세로가 0 입니다", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\SaveFile.json";

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "저장 파일 선택";
            dialog.FileName = "SaveFile";
            dialog.Filter = "저장 파일 (*.json) | *.json; | 모든 파일 (*.*) | *.*";


            if ((bool)dialog.ShowDialog())
            {
                using (StreamReader file = File.OpenText(dialog.FileName.ToString()))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject json = (JObject)JToken.ReadFrom(reader);


                    for (int j = 0; j < hei; j++)
                        for (int k = 0; k < wid; k++)
                            if (json["Data"][j][k].ToString() != "")
                                list.Add(new ChairModel(j, k, json["Data"][j][k].ToString(), check: true));
                            else
                                list.Add(new ChairModel(j, k, "", check: false));
                }
            }
        }
    }
}
