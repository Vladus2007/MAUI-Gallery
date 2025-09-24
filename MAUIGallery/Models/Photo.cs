


    
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text.Json.Serialization;

    namespace Gallery.Models
    {
        public class Photo : INotifyPropertyChanged
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("urls")]
            public Urls Urls { get; set; }

            [JsonPropertyName("alt_description")]
            public string AltDescription { get; set; }

            private bool _isFavorite;
            public bool IsFavorite
            {
                get => _isFavorite;
                set
                {
                    if (_isFavorite != value)
                    {
                        _isFavorite = value;
                        OnPropertyChanged();
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    


    public class Urls
    {
        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("full")]
        public string Full { get; set; }

        [JsonPropertyName("regular")]
        public string Regular { get; set; }

        [JsonPropertyName("small")]
        public string Small { get; set; }

        [JsonPropertyName("thumb")]
        public string Thumb { get; set; }
    }
}