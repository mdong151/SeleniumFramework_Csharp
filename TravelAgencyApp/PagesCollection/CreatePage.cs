﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravelAgencyApp.Ultilities;
using TravelAgencyApp.Objects;
using OpenQA.Selenium;

namespace TravelAgencyApp.PagesCollection
{
    public class CreatePage : CreatePageObjects
    {
        #region constant
        private int PAGE_TIME_OUT = 60;
        #endregion
        public void GoTo()
        {
            Browser.GoToPageWithCredentials("/create");
            Browser.WaitUntilElementIsInvisibled(LoaddingOverlayObject, PAGE_TIME_OUT);
        }
         
        public void FakeAuthenTo(string user)
        {
            string currentUser = Browser.GetText(UserMenuButton);
            if (!currentUser.Trim().ToLower().Contains(user.Trim().ToLower()))
            {
                Browser.Select(UserMenuButton);
                Browser.SearchAndSelect(UserMenuSearchField, user, PAGE_TIME_OUT);
                Browser.WaitUntilElementIsInvisibled(LoaddingOverlayObject, PAGE_TIME_OUT);
            }
        }

        public void FillPlanTrip(
            string travelType,
            string traveller,
            string mainTransportType,
            string startDate,string endDate,
            string fromPlace,string toPlace,
            string additionalServices = null
            )
        {
            //
            //TODO: inmplement Traveltype selection
            //

            
            //Browser.SearchAndSelect(TravellerSearchField, traveller, PAGE_TIME_OUT);
            //Browser.WaitUntilElementIsInvisibled(LoaddingOverlayObject, PAGE_TIME_OUT);
            
            if (mainTransportType.ToLower().Contains("any"))
            {
                Browser.Select(AnyTransportRadio);
                Browser.Select(AnyTransportFromPlaceField);
                Browser.SearchAndSelect(AnyTransportFromPlaceSearchField,fromPlace);
                Browser.Select(AnyTransportToPlaceField);
                Browser.SearchAndSelect(AnyTransportToPlaceSearchField,toPlace);

            }
            else if (mainTransportType.ToLower().Contains("plane"))
            {
                Browser.Select(PlaneRadio);
                Browser.Select(PlaneFromPlaceField);
                Browser.SearchAndSelect(PlaneFromPlaceSearchField,fromPlace);
                Browser.Select(PlaneToPlaceField);
                Browser.SearchAndSelect(PlaneToPlaceSearchField,toPlace);
            }
            else if (mainTransportType.ToLower().Contains("train"))
            {
                Browser.Select(TrainRadio);
                Browser.Select(TrainFromPlaceField);
                Browser.SearchAndSelect(TrainFromPlaceSearchField,fromPlace);
                Browser.Select(TrainToPlaceField);
                Browser.SearchAndSelect(TrainToPlaceSearchField,toPlace);
            }


            Browser.ClearAndEnterText(StartDateField,startDate);
            Browser.ClearAndEnterText(EndDateField,endDate);

            //
            //TODO : implement additional services
            //

        }
        public void SubmitPlanATripForm(bool isHurry = true)
        {
            if (isHurry == true)
            {
                Browser.Select(ImInAHurryButton);
            }
            else if (isHurry == false)
            {
                Browser.Select(FirstContinueButton);
            }
        }
        public string GetPopupMessage()
        {
            string message = Browser.GetText(PopupMessage);
            return message;
        }
 
    }
}