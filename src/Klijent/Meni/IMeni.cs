namespace Klijent.InterfejsMeni
{
    #region INTERFEJS ZA MODELOVANJE MENIJA KORISNIKU
    public interface IMeni
    {
        void IspisiMeni();

        void MeniSend();

        void MeniGet(string unos);
    }
    #endregion
}
