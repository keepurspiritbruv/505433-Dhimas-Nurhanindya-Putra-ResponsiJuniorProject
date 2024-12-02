using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace responsi
{
    internal class KaryawanRepository : Karyawan, IKaryawanRepository
    {
        private const string conn = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
        private static NpgsqlConnection? connection;
        private static NpgsqlCommand? cmd;
        private static DataTable? dt;

        private DataGridView dgvData;
        private DataGridViewRow? row;

        public DataGridViewRow? Row { set { row = value; } }

        public KaryawanRepository(DataGridView _dgvData)
        {
            this.dgvData = _dgvData;
            connection = new NpgsqlConnection(conn);
        }

        // LOAD
        public void Load()
        {
            try
            {
                connection?.Open();
                dgvData.DataSource = null;
                dt = new DataTable();
                string sql = "SELECT * FROM karyawan_select()";
                cmd = new NpgsqlCommand(sql, connection);

                using (NpgsqlDataReader rd = cmd.ExecuteReader())
                {
                    dt.Load(rd);
                }

                dgvData.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        // INSERT
        public void Insert(TextBox tbNamaKaryawan, ComboBox cbDepKaryawan)
        {
            try
            {
                connection?.Open();
                string sql = @"SELECT * FROM karyawan_insert(:_nama, :_id_dep)";
                cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue(":_nama", tbNamaKaryawan.Text);
                cmd.Parameters.AddWithValue(":_id_dep", int.Parse(cbDepKaryawan.Text));

                if ((int?)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data berhasil ditambahkan");
                    connection?.Close();
                    tbNamaKaryawan.Text = null;
                    cbDepKaryawan.Text = null;
                }
                else
                {
                    MessageBox.Show("Data gagal ditambahkan");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection?.Close();
            }
            finally
            {
                connection?.Close();
            }
        }

        // EDIT
        public void Edit(TextBox tbNamaKaryawan, ComboBox cbDepKaryawan)
        {
            if (row == null)
            {
                MessageBox.Show("Pilih data yang akan diubah");
                return;
            }

            try
            {
                connection?.Open();
                string sql = @"SELECT * FROM karyawan_update(:_id, :_nama, :_id_dep)";
                cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("_id", row.Cells["_id_karyawan"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("_nama", tbNamaKaryawan.Text);
                cmd.Parameters.AddWithValue("_id_dep", int.Parse(cbDepKaryawan.Text));

                if ((int?)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data berhasil diubah");
                    connection?.Close();
                    tbNamaKaryawan.Text = null;
                    cbDepKaryawan.Text = null;
                    row = null;
                }
                else
                {
                    MessageBox.Show("Data gagal diubah");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection?.Close();
            }
            finally
            {
                connection?.Close();
            }
        }

        // DELETE
        public void Delete(TextBox tbNamaKaryawan, ComboBox cbDepKaryawan)
        {
            if (row == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus");
                return;
            }

            try
            {
                connection?.Open();
                string sql = @"SELECT * FROM karyawan_delete(:_id)";
                cmd = new NpgsqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("_id", row.Cells["_id_karyawan"].Value?.ToString() ?? string.Empty);

                if ((int?)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Data berhasil dihapus");
                    connection?.Close();
                    tbNamaKaryawan.Text = null;
                    cbDepKaryawan.Text = null;
                    row = null;
                }
                else
                {
                    MessageBox.Show("Data gagal dihapus");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection?.Close();
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}