using ASUCloud.Model;

namespace ASUCloud.Web.Models
{
    public static class ViewModelConverter
    {
        public static User ToDomainUser(this LoginUserViewModel user)
        {
            return new User
            {
                Email = user.Email,
                Password = user.Password,

            };
        }

        public static User ToDomainUser(this RegisterUserViewModel user)
        {
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Icon = string.Empty,
                HasVerified = false
            };
        }

        public static UserDetailViewModel ToViewModel(this User user)
        {
            return new UserDetailViewModel
            {
                Name = user.Name,
                Email = user.Email,
                ID = user.ID,
                CreatedTime = user.CreatedTime,
                UpdatedTime = user.UpdatedTime,
            };
        }
    }
}
