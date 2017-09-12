using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public sealed class User
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFirstName", ErrorMessageResourceType = typeof(Properties.Resource))]
        [StringLength(255, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredLastName", ErrorMessageResourceType = typeof(Properties.Resource))]
        [StringLength(255, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredPatronymic", ErrorMessageResourceType = typeof(Properties.Resource))]
        [StringLength(255, MinimumLength = 1)]
        public string Patronymic { get; set; }

        [Required(ErrorMessageResourceName = "RequiredPhone", ErrorMessageResourceType = typeof(Properties.Resource))]
        [Phone(ErrorMessageResourceName = "NotValidPhone", ErrorMessageResourceType = typeof(Properties.Resource))]
        public string Phone { get; set; }
    }
}
