using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_4._2_
{
    public partial class Form1 : Form
    {
        Model model;
        public Form1()
        {
            InitializeComponent();
            model = new Model();
            model.observers += new System.EventHandler(this.Update);
        }

        private void Update(object sender, EventArgs e)
        {
            textBoxA.Text = model.getValueA().ToString();
            numericUpDownA.Value = model.getValueA();
            trackBarA.Value = model.getValueA();

            textBoxB.Text = model.getValueB().ToString();
            numericUpDownB.Value = model.getValueB();
            trackBarB.Value = model.getValueB();

            textBoxC.Text = model.getValueC().ToString();
            numericUpDownC.Value = model.getValueC();
            trackBarC.Value = model.getValueC();
        }

        class Model
        {
            private int[] value = new int[3] { 0, 0, 0 };

            public System.EventHandler observers;

            public Model()
            {
                value[0] = Properties.Settings.Default.val1;
                value[1] = Properties.Settings.Default.val2;
                value[2] = Properties.Settings.Default.val3;
            }
            public void setValueA(int value)
            {

                this.value[0] = value;
                if (this.value[0] > this.value[1])
                    this.value[1] = this.value[0];
                if (this.value[0] > this.value[2])
                    this.value[2] = this.value[0];

                observers.Invoke(this, null);

            }
            public void setValueB(int value)
            {
                int tmp = this.value[1];
                this.value[1] = value;
                if (!(this.value[0] <= this.value[1] && this.value[1] <= this.value[2]))
                    this.value[1] = tmp;

                observers.Invoke(this, null);


            }

            public void setValueC(int value)
            {
                this.value[2] = value;
                if (this.value[2] < this.value[1])
                    this.value[1] = this.value[2];
                if (this.value[2] < this.value[0])
                    this.value[0] = this.value[2];

                observers.Invoke(this, null);
            }

            public int getValueA()
            {
                return value[0];
            }
            public int getValueB()
            {
                return value[1];
            }
            public int getValueC()
            {
                return value[2];
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            model.observers.Invoke(this, null);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.val1 = model.getValueA();
            Properties.Settings.Default.val2 = model.getValueB();
            Properties.Settings.Default.val3 = model.getValueC();
            Properties.Settings.Default.Save();
        }

        //-------------------------textBox-----------------------------------------
        private void textBoxA_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
                model.setValueA(Int32.Parse(textBoxA.Text));
        }

        private void textBoxB_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
                model.setValueB(Int32.Parse(textBoxB.Text));
        }

        private void textBoxC_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
                model.setValueC(Int32.Parse(textBoxC.Text));
        }

        //------------------------numericUpDown-------------------------------------
        private void numericUpDownA_ValueChanged(object sender, EventArgs e)
        {
            model.setValueA(Decimal.ToInt32(numericUpDownA.Value));
        }

        private void numericUpDownB_ValueChanged(object sender, EventArgs e)
        {
            model.setValueB(Decimal.ToInt32(numericUpDownB.Value));
        }

        private void numericUpDownC_ValueChanged(object sender, EventArgs e)
        {
            model.setValueC(Decimal.ToInt32(numericUpDownC.Value));
        }

        //----------------------------trackBar--------------------------------------
        private void trackBarA_Scroll(object sender, EventArgs e)
        {
            model.setValueA(trackBarA.Value);
        }

        private void trackBarB_Scroll(object sender, EventArgs e)
        {
            model.setValueB(trackBarB.Value);
        }

        private void trackBarC_Scroll(object sender, EventArgs e)
        {
            model.setValueC(trackBarC.Value);
        }


    }
}
