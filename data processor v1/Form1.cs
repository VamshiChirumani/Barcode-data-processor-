using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using data_processor_v4a.Properties;

namespace data_processor_v4a
{
    public partial class Form1 : Form
    {
        SqlConnection myConn;

        double FirstDelta;
        double SecondDelta;
        double ThirdDelta;
        double ForthDelta;
        

        public class ScanEvent
        {
            public Int64 id;
            public string TestName;
            public string SymbolSet;
            public int TestSpeed;
            public string ScannerName;
            public DateTime _DateTime;
            public string ScannerData;
            public string EncodedData;
            public string ScanID;
            public double PhotoEyeSEC;
            public string ComputerName;
            public int TestAngle;
            public string TestDistance;
            public string Height;
            public double Delta1;
            public double Delta2;
            public double Delta3;
            public double Delta4;
        }

        List<ScanEvent> myScans = new List<ScanEvent>();
        List<ScanEvent> inputData = new List<ScanEvent>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try { this.Text += " (" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() + ")"; } catch { }

            txtConnection.Text = Settings.Default.Connection;
            if(txtConnection.TextLength>1)
            {
                try
                {
                    myConn = new SqlConnection(txtConnection.Text);
                    myConn.Open();
                    lblStatus.Text = "Connected...";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    btnRunQuery.Enabled = false;
                }

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Connection = txtConnection.Text;
            Settings.Default.Save();
            try { myConn.Close(); } catch { }
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "" || saveFileDialog1.FileName!="*.csv")
                txtFileName.Text = saveFileDialog1.FileName;
        }

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            myScans.Clear();
            lbTests.Items.Clear();

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string mySQL = "select distinct(data_testname) from testdata where _status is null order by data_testname";
                SqlCommand myCMD = new SqlCommand(mySQL, myConn);
                SqlDataReader results1 = myCMD.ExecuteReader();
                while(results1.Read())
                {
                    string test = results1[0].ToString();
                    lbTests.Items.Add(test);
                }
                results1.Close();
                myCMD.Dispose();
                //ProcessTests();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            lblStatus.Text = "Completed data processing...";
            Cursor.Current = Cursors.Default;
        }

        private void btnRunSelected_Click(object sender, EventArgs e)
        {
            #region Load data
            myScans.Clear();

            foreach (string test in lbTests.SelectedItems)
            {
                lblStatus.Text = "processing " + test;
                this.Refresh();
                try
                {
                    string sqlStr = "select * from testdata where data_testname like '" +
                                  test + "' and _status is null ";
                    if(cboxByID.Checked)
                        sqlStr = sqlStr + " order by id";
                    else
                        sqlStr = sqlStr + " order by data_datetime";

                    SqlCommand myCMD1 = new SqlCommand(sqlStr, myConn);
                    SqlDataReader result1 = myCMD1.ExecuteReader();
                    while (result1.Read())
                    {
                        ScanEvent tmp = new ScanEvent();
                        tmp.id = Convert.ToInt64(result1["id"].ToString());
                        tmp.TestName = result1["Data_TestName"].ToString();
                        //tmp.SymbolSet = result1["Data_SymbolSet"].ToString();
                        //tmp.TestSpeed = Convert.ToInt32(result1["data_testspeed"].ToString());
                        tmp.ScannerName = result1["data_scannername"].ToString();
                        tmp._DateTime = Convert.ToDateTime(result1["data_datetime"]);
                        tmp.ScannerData = result1["data_scannerdata"].ToString().Replace(',','~');
                        tmp.PhotoEyeSEC = Convert.ToDouble(result1["data_photoeyesec"]);
                        tmp.ComputerName = result1["data_computername"].ToString();

                        //tmp.EncodedData = StripID(tmp.ScannerData, tmp.ComputerName);

                        myScans.Add(tmp);
                    }
                    result1.Close();
                    myCMD1.Dispose();
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                }

            }
            #endregion

            ProcessTests();

            lblStatus.Text = "Done processing selected tests...";
        }

        private void btnRunAll_Click(object sender, EventArgs e)
        {
            #region Load data

            myScans.Clear();

            foreach (string test in lbTests.Items)
            {
                lblStatus.Text = "processing " + test;
                this.Refresh();
                try
                {
                    string sqlStr = "select * from testdata where data_testname like '" +
                                  test + "%' order by id";
                    SqlCommand myCMD1 = new SqlCommand(sqlStr, myConn);
                    SqlDataReader result1 = myCMD1.ExecuteReader();
                    while (result1.Read())
                    {
                        ScanEvent tmp = new ScanEvent();
                        tmp.id = Convert.ToInt64(result1["id"].ToString());
                        tmp.TestName = result1["Data_TestName"].ToString();
                        //tmp.SymbolSet = result1["Data_SymbolSet"].ToString();
                        //tmp.TestSpeed = Convert.ToInt32(result1["data_testspeed"].ToString());
                        tmp.ScannerName = result1["data_scannername"].ToString();
                        tmp._DateTime = Convert.ToDateTime(result1["data_datetime"].ToString());
                        tmp.ScannerData = result1["data_scannerdata"].ToString();
                        tmp.PhotoEyeSEC = Convert.ToDouble(result1["data_photoeyesec"]);
                        tmp.ComputerName = result1["data_computername"].ToString();

                        tmp.EncodedData = StripID(tmp.ScannerData, tmp.ComputerName);

                        myScans.Add(tmp);
                    }
                    result1.Close();
                    myCMD1.Dispose();
                }
                catch (Exception ex1)
                {
                    MessageBox.Show(ex1.Message);
                }

            }
            #endregion

            ProcessTests();

            lblStatus.Text = "Done processing all tests...";
        }

        private string StripID(string data, string computer)
        {
            string result = "";

            if (data == "Decode failed") return data;

            switch(computer)
            {
                case "ALPHA2":
                    if (data[0] == 'A')
                        result = data.Substring(1, data.Length - 1);
                    else if (data.Contains("P0G01"))
                        result = data.Substring(5, data.Length - 5);
                    else
                        result = data.Substring(3, data.Length - 3);
                    break;
                case "ALPHA5":
                    if(data.Contains("S08F]E") ==true)
                        result = data.Substring(7, data.Length - 7);
                    else
                        result = data.Substring(10, data.Length - 10);
                    break;
                case "DELTA0":
                    if(data.Contains("08F]E") == true)  //data.Contains("08FF]E")==true ||
                        result = data.Substring(6, data.Length - 6);
                    else if(data.Contains("S08Dm")==true || data.Contains("S08QR") == true)
                        result = data.Substring(5, data.Length - 5);
                    else if (data.Contains("S08G")==true)
                        result = data.Substring(4, data.Length - 4);
                    else
                        result = data.Substring(7, data.Length - 7);
                    break;
                case "DELTA1":
                    result = data.Substring(5, data.Length - 5);
                    break;
                case "BETA3":
                    if (data.Contains("08]Q0"))
                        result = data.Substring(3, data.Length - 3);
                    else if (data.Contains("]") == true)
                        result = data.Substring(7, data.Length - 7);
                    else 
                        result = data.Substring(3, data.Length - 3);
                    
                    break;
            }

            string myChar = Convert.ToChar(29).ToString();
            if (result.Contains(myChar))
                result = result.Replace(myChar, "<GS>");

            if (result.Contains("https://"))
                result = result.Substring(result.Length - 12, 12);

            return result;
        }

        private string StripScanID(string data)
        {
            string result = "";

            if(data.Contains('~'))
            {
                int _pos = data.IndexOf('~');
                result = data.Substring(_pos-8, 8);  //8 digits only
            }

            if(data.Contains("S08$n"))
            {
                //S08$n00006779G09521722001195
                result = data.Substring(5, 8);
            }

            return result;
        }

        private string GetEncodedData(string data)
        {
            string result = "";

            if (data.Contains('~'))
            {
                int _pos = data.IndexOf('~');
                //P0067454187~123456789DM1
                result = data.Substring(_pos + 1,data.Length - (_pos+1) );  //8 digits only
            }

            if (data.Contains("S08$n"))
            {
                //S08$n00006779G09521722001195
                result = data.Substring(14, data.Length-14);
            }
            //if (data.Contains("123456789DM1"))
            //    MessageBox.Show("");

            if (result.Contains("G]Q3"))
                result = result.Substring(4, result.Length - 4);
            else if (result.Contains("QR"))
                result = result.Substring(4, result.Length - 4);

            return result.Replace("^", "<GS>");
        }
        private void ProcessTests()
        {
            #region UpdateSpeeds

            string[] mySpeeds = txtSpeedList.Text.Split(',');
            string[] myHeights = txtHeights.Text.Split(',');
            string[] myOffsets = txtOffsets.Text.Split(',');

            string cardID = "";
            int speed = 0;
            int height = 0;
            int offset = 0;
            bool FirstOccurance = false;

            try
            {
                for (int i = 0; i < myScans.Count; i++)
                {
                    if (i == 0)
                    {
                        if (myScans[i].ScannerData.Contains("ID"))
                            cardID = myScans[i].ScannerData;
                        else
                            cardID = myScans[i].ScannerData.Substring(myScans[i].ScannerData.Length - 12, 12);
                    }

                    //if (myScans[i].ScannerName == "delimiter" && myScans[i].ScannerData.Contains(cardID))
                    if (myScans[i].ComputerName == "ALPHA4" && myScans[i].ScannerData.Contains(cardID))
                    {
                        if (i > 1)
                            speed++;

                        if (mySpeeds[speed] == "150" && i > 1)
                            height++;
                    }


                    //if (myScans[i].ScannerName == "delimiter" && myScans[i].ScannerData != cardID)

                    if (myScans[i].ComputerName == "ALPHA4")
                    {
                        if (myScans[i].ScannerData.Contains(cardID) == false)
                        {
                            speed = 0;
                            if (myScans[i].ScannerData.Contains("ID"))
                                cardID = myScans[i].ScannerData;
                            else
                                cardID = myScans[i].ScannerData.Substring(myScans[i].ScannerData.Length - 12, 12);
                            height = 0;
                        }
                    }

                    //reset condition exists if cardid isn't in ScannerData
                    //if(myScans[i].ComputerName != "ALPHA4" && myScans[i].ScannerData!= "Decode failed" && myScans[i].ScannerData.Contains(cardID)==false)
                    //{
                    //    speed = 0;
                    //    //cardID = myScans[i].ScannerData.Substring(myScans[i].ScannerData.Length - 13, 13);  //StripID(myScans[i].ScannerData, myScans[i].ComputerName);
                    //    cardID = StripID(myScans[i].ScannerData, myScans[i].ComputerName);
                    //    cardID = cardID.Substring(cardID.Length - 12, 12);
                    //    height = 0;
                    //}

                    //myScans[i].ScannerData = myScans[i].ScannerData.Replace(((char)0x29).ToString(), "<gs>");
                    myScans[i].EncodedData = GetEncodedData(myScans[i].ScannerData);
                    myScans[i].ScanID = StripScanID(myScans[i].ScannerData);
                    myScans[i].TestSpeed = Convert.ToInt32(mySpeeds[speed]);
                    myScans[i].Height = myHeights[height];
                    myScans[i].SymbolSet = cardID;
                }
            }
            catch (Exception ex3)
            {
                lblStatus.Text = ex3.Message;
            }
            var comp = myScans.GroupBy(x => x.ComputerName).Select(x => x.First()).ToList();

            //process intervals if more than one decode occured
            foreach (ScanEvent computer in comp)
            {
                string LastScanner = "";

                for (int j = 0; j < myScans.Count; j++)
                    if (myScans[j].ComputerName == computer.ComputerName)
                    {
                        if (myScans[j].ScannerData == "PE event" || myScans[j].ScannerName == "delimiter")
                        {
                            FirstDelta = 0.0;
                            SecondDelta = 0.0;
                            ThirdDelta = 0.0;
                            ForthDelta = 0.0;
                        }
                        else
                        {
                            if (myScans[j].ScannerName != "Alpha4") // && myScans[i].ComputerName == LastScanner)
                            {
                                if (FirstDelta == 0.0)
                                {
                                    FirstDelta = myScans[j].PhotoEyeSEC;
                                }
                                else if (SecondDelta == 0.0)
                                {
                                    SecondDelta = myScans[j].PhotoEyeSEC - FirstDelta;
                                    myScans[j].Delta1 = SecondDelta;
                                }
                                else if (ThirdDelta == 0.0)
                                {
                                    ThirdDelta = myScans[j].PhotoEyeSEC - (SecondDelta + FirstDelta);
                                    myScans[j].Delta2 = ThirdDelta;
                                }
                                else
                                {
                                    ForthDelta = myScans[j].PhotoEyeSEC - (SecondDelta + FirstDelta + ThirdDelta);
                                    myScans[j].Delta3 = ForthDelta;
                                }
                            }
                            LastScanner = myScans[j].ComputerName;
                        }
                    }            
            }

            #endregion

            #region Export Data
            if(txtFileName.TextLength>0)
            {
                using (System.IO.StreamWriter output = new System.IO.StreamWriter(txtFileName.Text))
                {
                    output.WriteLine("id,date time,test,speed,height,card,computer,data,Encoded_data,Scan ID,time,scanner,Interval 1,Interval 2,Interval 3,Interval 4, Filter 1, Filter 2, Filter 3, Filter 4, Filter 5, Filter 6");
                    try
                    {
                        foreach (ScanEvent scan in myScans)
                        {
                            if (scan.ScannerData != "Decode failed")
                            {
                                output.Write(scan.id.ToString() + ",");
                                output.Write(scan._DateTime.ToString("MM/dd/yyyy HH:mm:ss.FFF") + ",");
                                output.Write(scan.TestName + ",");
                                output.Write(scan.TestSpeed.ToString() + ",");
                                output.Write(scan.Height + ",");
                                output.Write(scan.SymbolSet + ",");
                                output.Write(scan.ComputerName + ",");
                                output.Write(scan.ScannerData.Replace('\n',' ').Trim() + ",");
                                
                                output.Write(scan.EncodedData + ",");                                

                                output.Write(scan.ScanID + ",");
                                
                                if(scan.PhotoEyeSEC!=0)
                                    output.Write(scan.PhotoEyeSEC.ToString() + ",");                                                               
                                else
                                    output.Write(",");

                                output.Write(scan.ScannerName + "," );
                                if(scan.Delta1==0)
                                    output.Write(",");
                                else
                                    output.Write(scan.Delta1 + ",");
                                if (scan.Delta2 == 0)
                                    output.Write(",");
                                else
                                    output.Write(scan.Delta2 + ",");
                                if (scan.Delta3 == 0)
                                    output.Write(",");
                                else
                                    output.Write(scan.Delta3 + ",");
                                if (scan.Delta4 == 0)
                                    output.WriteLine(",");
                                else
                                    output.WriteLine(scan.Delta4 + ",");
                            }
                            if (cbAnalysis.Checked)
                            {
                                try
                                {
                                    string mySQL = "insert into analysis (id,datetimestamp, test, speed, height, card, computer, data, encodeddata,decodetime) values ('" +
                                                    scan.id + "','" +
                                                    scan._DateTime.ToString("MM/dd/yyyy HH:mm:ss.FFF") + "','" +
                                                    scan.TestName + "','" +
                                                    scan.TestSpeed.ToString() + "','" +
                                                    scan.Height + "','" +
                                                    scan.SymbolSet + "','" +
                                                    scan.ComputerName + "','" +
                                                    scan.ScannerData + "','" +
                                                    scan.EncodedData + "','" +
                                                    scan.PhotoEyeSEC.ToString() + "')";
                                    SqlCommand myCMD = new SqlCommand(mySQL, myConn);
                                    myCMD.ExecuteNonQuery();
                                    myCMD.Dispose();
                                }
                                catch (Exception ex3) { MessageBox.Show(ex3.Message); }
                            }
                        }
                    }
                    catch { }

                    string fileparts = txtFileName.Text.Substring(0,txtFileName.Text.Length-4);

                    using (System.IO.StreamWriter output2 = new System.IO.StreamWriter(fileparts + "-pe.csv"))
                    {
                        output2.WriteLine("id,date time,test,speed,height,card,computer,data,Encoded_data,Scan ID,time,scanner,Interval 1,Interval 2,Interval 3,Interval 4, Filter 1, Filter 2, Filter 3, Filter 4, Filter 5, Filter 6");

                        foreach (ScanEvent scan in myScans)
                        {
                            if (scan.ScannerData != "Decode failed" && (scan.ScannerData=="PE event" || scan.ComputerName=="ALPHA4" )) 
                            {
                                output2.Write(scan.id.ToString() + ",");
                                output2.Write(scan._DateTime.ToString("MM/dd/yyyy HH:mm:ss.FFF") + ",");
                                output2.Write(scan.TestName + ",");
                                output2.Write(scan.TestSpeed.ToString() + ",");
                                output2.Write(scan.Height + ",");
                                output2.Write(scan.SymbolSet + ",");
                                output2.Write(scan.ComputerName + ",");
                                output2.Write(scan.ScannerData.Replace('\n', ' ').Trim() + ",");

                                output2.Write(scan.EncodedData + ",");

                                output2.Write(scan.ScanID + ",");
                                output2.Write(scan.PhotoEyeSEC.ToString() + ",");
                                output2.Write(scan.ScannerName + ",");
                                if (scan.Delta1 == 0)
                                    output2.Write(",");
                                else
                                    output2.Write(scan.Delta1 + ",");
                                if (scan.Delta2 == 0)
                                    output2.Write(",");
                                else
                                    output2.Write(scan.Delta2 + ",");
                                if (scan.Delta3 == 0)
                                    output2.Write(",");
                                else
                                    output2.Write(scan.Delta3 + ",");
                                if (scan.Delta4 == 0)
                                    output2.WriteLine(",");
                                else
                                    output2.WriteLine(scan.Delta4 + ",");
                            }
                        }

                    }
                }
            }

            #endregion
        }

        private void txtSpeedList_Leave(object sender, EventArgs e)
        {
            txtSpeedList.Text = txtSpeedList.Text.Replace(" ", "");
        }

        class range
        {
            public string minID;
            public string maxID;
            public string speed;
            public string card;
            public string height;
            public TimeSpan duration;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<range> myCards = new List<range>();

            string mySQL = "select min(id) as 'first', max(id) as 'last' , speed, card, height " +
                           "from analysis group by card, speed, height order by card, speed, height";

            SqlCommand myCMD = new SqlCommand(mySQL, myConn);
            SqlDataReader myReader = myCMD.ExecuteReader();
            while (myReader.Read())
            {
                range myRange = new range();
                myRange.minID = myReader[0].ToString();
                myRange.maxID = myReader[1].ToString();
                myRange.speed = myReader[2].ToString();
                myRange.card = myReader[3].ToString();
                myRange.height = myReader[4].ToString();

                myCards.Add(myRange);
            }
            myReader.Close();
            myCMD.Dispose();

            List<range> myResults = new List<range>();

            foreach (range card in myCards)
            {
                string mySQL2 = "select * from analysis where id in (" + card.minID + ", " + card.maxID + ")";
                SqlCommand myCMD2 = new SqlCommand(mySQL2, myConn);
                SqlDataReader myReader2 = myCMD2.ExecuteReader();
                DateTime first=new DateTime();
                DateTime second = new DateTime();
                while(myReader2.Read())
                {
                    if (first == Convert.ToDateTime("1/1/0001 12:00:00 AM")) 
                        first = Convert.ToDateTime(myReader2[1].ToString());
                    else
                        second = Convert.ToDateTime(myReader2[1].ToString());
                }
                myReader2.Close();
                myCMD2.Dispose();
                range tmp = new range();
                tmp.card = card.card;
                tmp.speed = card.speed;
                tmp.height = card.height;
                tmp.duration = second.Subtract(first);
                myResults.Add(tmp);
            }
            using(System.IO.StreamWriter _out = new System.IO.StreamWriter("spans.txt",false))
            {
                _out.WriteLine("card, speed, height, duration");
                foreach (range data in myResults)
                {
                    _out.Write(data.card.ToString().Trim() + ",");
                    _out.Write(data.speed.ToString().Trim() + ",");
                    _out.Write(data.height.ToString().Trim() + ","); 
                    _out.WriteLine(data.duration.TotalMilliseconds.ToString());
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        /*
           List<ScanEvent> myTMP = new List<ScanEvent>();
           openFileDialog1.FileName = "";
           openFileDialog1.ShowDialog();
           if(openFileDialog1.FileName.Length>1)
           {
               try
               {
                   using (System.IO.StreamReader inFile = new System.IO.StreamReader(openFileDialog1.FileName))
                   {
                       while (inFile.Peek() != -1)
                       {
                           string[] data = inFile.ReadLine().Split(',');
                           ScanEvent tmp = new ScanEvent();
                           tmp.id = Convert.ToInt64(data[0]);
                           tmp.TestName = data[3];
                           tmp.ScannerName = data[7];
                           tmp._DateTime = Convert.ToDateTime(data[8]);
                           tmp.ScannerData = data[9];
                           tmp.PhotoEyeSEC = Convert.ToDouble(data[12]);
                           tmp.ComputerName = data[13];
                           myTMP.Add(tmp);                                
                       }
                   }

               }
               catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
           }

           string[] mySpeeds = txtSpeedList.Text.Split(',');
           string[] myHeights = txtHeights.Text.Split(',');


           string cardID = "";
           int speed = 0;
           int height = 0;
           bool FirstOccurance = false;

           try
           {
               for (int i = 0; i < myTMP.Count; i++)
               {
                   if (myTMP[i].ScannerName == "delimiter" && myTMP[i].ScannerData == cardID)
                   {
                       speed++;

                       if (mySpeeds[speed] == "150" && i > 1)
                           height++;
                   }
                   if (i == 7158)
                       MessageBox.Show("");

                   if (myTMP[i].ScannerName == "delimiter" && myTMP[i].ScannerData != cardID)
                   {
                       speed = 0;
                       cardID = myTMP[i].ScannerData;
                       height = 0;
                   }

                   myTMP[i].TestSpeed = Convert.ToInt32(mySpeeds[speed]);
                   myTMP[i].Height = myHeights[height];
                   myTMP[i].SymbolSet = cardID;
                   System.Diagnostics.Debug.WriteLine("i: " + i.ToString());
               }
           }
           catch (Exception ex3)
           {
               lblStatus.Text = ex3.Message;
           }

       */
    }
}
