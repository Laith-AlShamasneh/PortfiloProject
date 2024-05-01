using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Portfilo.Models;
using Portfilo.Models.Repository;
using Portfilo.ViewModels;

namespace Portfilo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Menu> menu;
        private readonly IRepository<Header> header;
        private readonly IRepository<About> about;
        private readonly IRepository<SocialMedia> media;
        private readonly IRepository<Experience> experience;
        private readonly IRepository<Education> education;
        private readonly IRepository<ProfessionalSkill> skill;
        private readonly IRepository<Language> lang;
        private readonly IRepository<Project> project;
        private readonly IRepository<CallToAction> call;
        private readonly IRepository<TransactionContactUs> contact;

        public HomeController(IRepository<Menu> menu,
            IRepository<Header> header,
            IRepository<About> about,
            IRepository<SocialMedia> media,
            IRepository<Experience> experience,
            IRepository<Education> education,
            IRepository<ProfessionalSkill> skill,
            IRepository<Language> lang,
            IRepository<Project> project,
            IRepository<CallToAction> call,
            IRepository<TransactionContactUs> contact)
        {
            this.menu = menu;
            this.header = header;
            this.about = about;
            this.media = media;
            this.experience = experience;
            this.education = education;
            this.skill = skill;
            this.lang = lang;
            this.project = project;
            this.call = call;
            this.contact = contact;
        }
        public async Task<IActionResult> Index()
        {
            var data = new Home
            {
                MenuList = await menu.GetAllForClientAsync(),
                SocialMediaList = await media.GetAllForClientAsync(),
                Headers = await header.FindAsync(1),
                Abouts = await about.FindAsync(1),
            };
            return View(data);
        }

        public async Task<IActionResult> Resume()
        {
            var data = new Home
            {
                MenuList = await menu.GetAllForClientAsync(),
                ExperienceList = await experience.GetAllForClientAsync(),
                EducationList = await education.GetAllForClientAsync(),
                ProfessionalSkillList = await skill.GetAllForClientAsync(),
                LanguageList = await lang.GetAllForClientAsync(),
            };
            return View(data);
        }

        public async Task<IActionResult> Project()
        {
            var data = new Home
            {
                MenuList = await menu.GetAllForClientAsync(),
                ProjectList = await project.GetAllForClientAsync(),
                CallToActions = await call.FindAsync(1),
            };
            return View(data);
        }

        public async Task<IActionResult> Contact()
        {
            var data = new Home
            {
                MenuList = await menu.GetAllForClientAsync(),
            };
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddContact(Home collection)
        {

            collection.TransactionContactUss.CreateDate = DateTime.UtcNow;
            await contact.AddAsync(collection.TransactionContactUss);
            return RedirectToAction(nameof(Index));
        }
    }
}
