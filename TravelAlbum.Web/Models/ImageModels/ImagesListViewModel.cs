using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TravelAlbum.Models;
using TravelAlbum.Resources.App_GlobalResources;
using TravelAlbum.Web.Enums;

namespace TravelAlbum.Web.Models.ImageModels
{
    public class ImagesListViewModel
    {
        // private ICollection<Mountain> mountains;       

        public ImagesListViewModel()
        {
            this.ImagePreviews = new List<ImagePreviewOutputViewModel>();
            // this.Mountains = new HashSet<Mountain>();
            // this.Mountains = new HashSet<Mountains>();
        }

        [Display(Name = "MountainSearchLabel", ResourceType = typeof(GlobalResources))]
        public IEnumerable<Guid> MountainsIds { get; set; }

        public IEnumerable<SelectListItem> MountainsDropDown { get; set; }

        public List<ImagePreviewOutputViewModel> ImagePreviews { get; set; }

        public Sorting SelectedSorting { get; set; }

        public int SelectedPageSize { get; set; }

        public IEnumerable<SelectListItem> PageSizes { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }       
    }
}