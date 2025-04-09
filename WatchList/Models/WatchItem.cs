using System.ComponentModel.DataAnnotations;

namespace WatchList.Models
{
    public class WatchItem
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string Title { get; set; }  

        [Required(ErrorMessage = "Тип обязателен")]
        public string Type { get; set; }  

        [Required(ErrorMessage = "Статус обязателен")]
        public string Status { get; set; }  
    }
}