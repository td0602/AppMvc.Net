using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Controller_View.Models.Contacts {
    public class Contact {
        [Key]
        public int Id {get; set;}
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Phải nhập {0}")] // thông báo lỗi khi check valid trong form
        [Display(Name = "Họ tên")]
        public string FullName {set; get;}
        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Phải là địa chỉ Email")]
        [Display(Name = "Địa chỉ Email")]
        public string Email {set; get;}
        public DateTime DateSent {set; get;}
        [Display(Name = "Nội dung")]
        public string Message {get; set;}
        [StringLength(50)]
        [Phone(ErrorMessage = "Phải là số điện thoại")]
        [Display(Name = "Số điện thoại")]
        public string Phone {set; get;}
    }
}