using System.Data;

namespace responsi
{
    public partial class Form1 : Form
    {
        private DataTable dt;
        private DataGridViewRow r;

        private Karyawan karyawan;
        private KaryawanRepository karyawanRepository;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            karyawanRepository.Insert(tbNamaKaryawan, cbDepKaryawan);
            karyawanRepository.Load();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            karyawanRepository = new KaryawanRepository(dgvData);
            karyawan = new Karyawan();
            karyawanRepository.Load();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            karyawanRepository.Edit(tbNamaKaryawan, cbDepKaryawan);
            karyawanRepository.Load();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            karyawanRepository.Delete(tbNamaKaryawan, cbDepKaryawan);
            karyawanRepository.Load();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=  0)
            {
                r = dgvData.Rows[e.RowIndex];
                tbNamaKaryawan.Text = r.Cells["_nama"].Value.ToString();
                cbDepKaryawan.Text = r.Cells["_id_dep"].Value.ToString();
                karyawanRepository.Row = r;
                karyawan.Nama = tbNamaKaryawan.Text;
            }
        }
    }
}
