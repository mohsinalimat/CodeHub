using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using CodeHub.Core.ViewModels;
using CodeHub.Core.ViewModels.User;
using GitHubSharp.Models;

namespace CodeHub.Core.ViewModels.Repositories
{
    public class RepositoryCollaboratorsViewModel : LoadableViewModel
    {
        private readonly CollectionViewModel<BasicUserModel> _collaborators = new CollectionViewModel<BasicUserModel>();

        public CollectionViewModel<BasicUserModel> Collaborators
        {
            get { return _collaborators; }
        }

        public string Username { get; private set; }

        public string Repository { get; private set; }

        public ICommand GoToUserCommand
        {
            get { return new MvxCommand<BasicUserModel>(x => this.ShowViewModel<ProfileViewModel>(new ProfileViewModel.NavObject { Username = x.Login })); }
        }

        public void Init(NavObject navObject) 
        {
            Username = navObject.Username;
            Repository = navObject.Repository;
        }

        protected override Task Load(bool forceDataRefresh)
        {
            return Collaborators.SimpleCollectionLoad(this.GetApplication().Client.Users[Username].Repositories[Repository].GetCollaborators(), forceDataRefresh);
        }

        public class NavObject
        {
            public string Username { get; set; }
            public string Repository { get; set; }
        }
    }
}

