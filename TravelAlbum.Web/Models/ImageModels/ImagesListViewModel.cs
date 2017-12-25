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
            this.singleImagePreviews = new List<ImagePreviewOutputViewModel>();
            // this.Mountains = new HashSet<Mountain>();
            // this.Mountains = new HashSet<Mountains>();
        }

        [Display(Name = "MountainSearchLabel", ResourceType = typeof(GlobalResources))]
        public IEnumerable<Guid> MountainsIds { get; set; }

        public IEnumerable<SelectListItem> MountainsDropDown { get; set; }

        public List<ImagePreviewOutputViewModel> singleImagePreviews { get; set; }

        public Mountain SelectedMountain { get; set; }

        public Guid SelectedMountainId { get; set; }

        public Sorting SelectedSorting { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        // public virtual ICollection<Mountain> Mountains
        // {
        //     get { return this.mountains; }
        //     set { this.mountains = value; }
        // }
    }
}