using EFStudy.Models;
using Microsoft.EntityFrameworkCore;

namespace EFStudy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var context = new EFDBContext())
            {
                //���� DB�� ������ ��� ����
                bool deleted = context.Database.EnsureDeleted();

                //Model�� ���� DB�� ����� �ʿ��� SQL Script�� ����
                bool created = context.Database.EnsureCreated();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var context = new EFDBContext())
            {
            }
        }
    }
}