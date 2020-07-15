using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Computers.ViewModels.Stubs
{
    public class DetailsViewModel : IDetailsViewModel
    {
        public string Name => "Intel Core i7-8700k";

        public string ImagePath => "http://www.hfinformatique.be/images/produits/17609.jpg";

        public ObservableCollection<IInfoViewModel> Infos => new ObservableCollection<IInfoViewModel>() 
        {
            new InfoViewModelStub() {Icon="https://cdn2.iconfinder.com/data/icons/miscellaneous-i-fill-style/150/factory-512.png", Key="Fabricant", Value="Intel"},
            new InfoViewModelStub() {Icon="/Computers;component/Rsc/coeur.png", Key="# Coeurs", Value="4"},
            new InfoViewModelStub() {Icon="https://cdn3.iconfinder.com/data/icons/audio-and-video-1-1/512/1-512.png", Key="Base Freq.", Value="3.70 Ghz"},
            new InfoViewModelStub() {Icon="https://banner2.cleanpng.com/20180720/wsv/kisspng-secure-digital-computer-data-storage-flash-memory-material-design-email-icon-5b518910aafd68.0406245615320701607004.jpg", Key="Cache", Value="12 Mo"},
            new InfoViewModelStub() {Icon="https://img.icons8.com/material-rounded/96/000000/price-tag-euro.png", Key="Prix", Value="429,00 €"},
        };

        public ICommand Exporter => throw new System.NotImplementedException();

        public void Export()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class InfoViewModelStub : IInfoViewModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
    }
}