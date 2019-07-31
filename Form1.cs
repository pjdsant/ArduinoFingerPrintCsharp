using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace YouPlay
{
    public partial class Form1 : Form
    {

        string RxString;

        public Form1()
        {
            InitializeComponent();
        }

        private void atualizaListaCOMs()
        {
            int i;

            bool differentAmount;

            i = 0;
            differentAmount = false;

            if (comboBox1.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (comboBox1.Items[i++].Equals(s) == false)
                    {
                        differentAmount = true;
                    }
                }
            }
            else
            {
                differentAmount = true;
            }

            if (differentAmount == false)
            {
                return;                     //retorna
            }

            comboBox1.Items.Clear();

            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }

            comboBox1.SelectedIndex = 0;

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TimerCOM_Tick(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    serialPort1.Open();
                }
                catch
                {
                    return;

                }
                if (serialPort1.IsOpen)
                {
                    btnConnect.Text = "Disconnect";
                    comboBox1.Enabled = false;

                }
            }
            else
            {

                try
                {
                    serialPort1.Close();
                    comboBox1.Enabled = true;
                    btnConnect.Text = "Connect";
                }
                catch
                {
                    return;
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Load("C:\\repositorio\\bio.jpeg");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)  // se porta aberta
                serialPort1.Close();         //fecha a porta
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)          //porta está aberta
                serialPort1.Write(textBoxSend.Text);
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            pictureBox1.InitialImage = null;

            RxString = serialPort1.ReadExisting();              //le o dado disponível na serial

            this.Invoke(new EventHandler(trataDadoRecebido));

            if (RxString == "2000" || RxString == "1000")
            {
                pictureBox1.Load("C:\\repositorio\\perfilfoto.jpg");
            }
            //this.Invoke(new EventHandler(trataDadoRecebido));   //chama outra thread para escrever o dado no text box
        }

        private void trataDadoRecebido(object sender, EventArgs e)
        {
            pictureBox1.Load("C:\\repositorio\\bio.jpeg");

            textBoxReceive.AppendText(RxString);

            //if (RxString.Contains("#1") || RxString.Contains("#1"))
            //{
           // pictureBox1.Load("C:\\repositorio\\perfilfoto.jpg");
            //}

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // pictureBox1.Load("C:\\repositorio\\perfilfoto.jpg");

            // pictureBox1.Image = null;

            pictureBox1.Load("C:\\repositorio\\bio.jpeg");

        }

        private void PictureBox1_Resize(object sender, EventArgs e)
        {

        }
    }
}
