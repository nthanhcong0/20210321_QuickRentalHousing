using QuickRentalHousing.Models.Districts;
using QuickRentalHousing.Models.Genders;
using QuickRentalHousing.Models.Homeowners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuickRentalHousing.FE.Pages
{
    public partial class Homeowners
    {
        private IEnumerable<GenderSelectionRespondModel> _genderSelectionModels = Array.Empty<GenderSelectionRespondModel>();
        private IEnumerable<DistrictSelectionRespondModel> _districtSelectionModels = Array.Empty<DistrictSelectionRespondModel>();
        private IEnumerable<HomeownerRespondModel> _homeownerModel = Array.Empty<HomeownerRespondModel>();

        private Guid? _selectedId;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        [Required]
        [Display(Name = "Personal Id")]
        public string PID { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime? DOB { get; set; }
        [Required]
        [Display(Name = "Address Number")]
        public string AddressNumber { get; set; }
        [Required]
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        [Display(Name = "District")]
        public int DistrictId { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string Description { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _genderSelectionModels = await _genderModuleService.GetSelectionModelsAsync();
            _districtSelectionModels = await _districtModuleService.GetSelectionModelsAsync();
            Reset();
            await LoadAllAsync();
        }

        private async Task Submit()
        {
            if (_selectedId.HasValue)
            {
                await _homeownersService.UpdateAsync(_selectedId.Value,
                    FirstName, MiddleName, LastName, GenderId,
                    PID, DOB.Value, AddressNumber, StreetName,
                    DistrictId, new string[] { PhoneNumber },
                    new string[] { Email }, Description);
            }
            else
            {
                await _homeownersService.CreateAsync(
                    FirstName, MiddleName, LastName, GenderId,
                    PID, DOB.Value, AddressNumber, StreetName,
                    DistrictId, new string[] { PhoneNumber },
                    new string[] { Email }, Description);
            }

            Reset();
            await LoadAllAsync();
        }

        private async Task LoadAllAsync()
        {
            _homeownerModel = await _homeownersService.LoadAllAsync();
        }

        private async Task LoadAsync(Guid id)
        {
            var homeownerDetailModel = await _homeownersService.LoadAsync(id);

            _selectedId = homeownerDetailModel.Id;
            FirstName = homeownerDetailModel.FirstName;
            MiddleName = homeownerDetailModel.MiddleName;
            LastName = homeownerDetailModel.LastName;
            GenderId = homeownerDetailModel.Gender.Id;
            PID = homeownerDetailModel.PID;
            DOB = homeownerDetailModel.DOB;
            AddressNumber = homeownerDetailModel.AddressNumber;
            StreetName = homeownerDetailModel.Street.Name;
            DistrictId = homeownerDetailModel.District.Id;
            PhoneNumber = homeownerDetailModel.PhoneNumbers?.First()?.PhoneNumber;
            Email = homeownerDetailModel.Emails?.First()?.Email;
            Description = homeownerDetailModel.Description;
        }

        private async Task RemoveAsync(Guid id)
        {
            await _homeownersService.RemoveAsync(id);
            await LoadAllAsync();
        }

        private void Reset()
        {
            _selectedId = null;
            FirstName = string.Empty;
            MiddleName = string.Empty;
            LastName = string.Empty;
            GenderId = _genderSelectionModels.First().Id;
            PID = string.Empty;
            DOB = null;
            AddressNumber = string.Empty;
            StreetName = string.Empty;
            DistrictId = _districtSelectionModels.First().Id;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            Description = string.Empty;
        }
    }
}
