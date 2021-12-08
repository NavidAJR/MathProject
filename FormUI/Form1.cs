using MathExpressionEvaluator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathProject.DataLayer.Context;

namespace FormUI
{
    public partial class Form1 : Form
    {
        private ExpressionEvaluator _expressionEvaluator;
        private DataBaseContext _context;
        public Form1()
        {
            _context = new DataBaseContext();
            _expressionEvaluator = new ExpressionEvaluator(_context);
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var input = txtInput.Text;
            lblResultValue.Text = _expressionEvaluator.Evaluate(input).ToString();
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                SendKeys.Send("{TAB}");
                e.SuppressKeyPress = true;
            }
        }
    }
}
