namespace responsi
{
    public interface IKaryawanRepository
    {
        void Load();
        void Insert(TextBox tbNamaKaryawan, ComboBox cbDepKaryawan);
        void Edit(TextBox tbNamaKaryawan, ComboBox cbDepKaryawan);
        void Delete(TextBox tbNamaKaryawan, ComboBox cbDepKaryawan);
        DataGridViewRow Row { set; }
    }
}