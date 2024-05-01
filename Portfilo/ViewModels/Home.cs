using Portfilo.Models;

namespace Portfilo.ViewModels
{
    public class Home
    {
        public IEnumerable<Menu> MenuList { get; set; }
        public IEnumerable<SocialMedia> SocialMediaList { get; set; }
        public IEnumerable<Experience> ExperienceList { get; set; }
        public IEnumerable<Education> EducationList { get; set; }
        public IEnumerable<ProfessionalSkill> ProfessionalSkillList { get; set; }
        public IEnumerable<Language> LanguageList { get; set; }
        public IEnumerable<Project> ProjectList { get; set; }
        public Header Headers { get; set; } = new Header();
        public About Abouts { get; set; } = new About();
        public CallToAction CallToActions { get; set; } = new CallToAction();
        public TransactionContactUs TransactionContactUss { get; set; } = new TransactionContactUs();
    }
}
