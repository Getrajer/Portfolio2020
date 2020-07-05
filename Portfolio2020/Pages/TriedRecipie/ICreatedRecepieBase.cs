using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Pages.DataSharp
{
    public class ICreatedRecepieBase : ComponentBase
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        #region UserRecepie
        public class UserRecepies
        {
            public int RecepiesId { get; set; }
            public int UserId { get; set; }

            /// <summary>
            /// This list will hold ongoing recepies of user
            /// </summary>
            public List<Recepie> Ongoing_Recepies = new List<Recepie>();

            /// <summary>
            /// This list will hold list of recepies user thinks is perfect
            /// </summary>
            public List<Recepie> Finished_Recepies = new List<Recepie>();

            /// <summary>
            /// This list will hold recepies of user who thinks there is no hope in this recepie
            /// </summary>
            public List<Recepie> Abandoned_Recepies = new List<Recepie>();

            public UserRecepies() { }

        }
        #endregion

        #region Recepie class
        public class Recepie
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            /// <summary>
            /// Each number will idicate type for the icon: 
            /// 1 - Breakfast
            /// 2 - Lunch
            /// 3 - Dinner
            /// 4 - Dessert
            /// 5 - Snack
            /// </summary>
            public int Type { get; set; }

            /// <summary>
            /// From 1 - 10
            /// </summary>
            public int Rating { get; set; }

            public string Comment { get; set; }

            /// <summary>
            /// If user is satisfied with the end result
            /// </summary>
            public bool Finished { get; set; }

            /// <summary>
            /// If user is still working on perfecting the recepie
            /// </summary>
            public bool Ongoing { get; set; }

            /// <summary>
            /// If user thinks there is no home in this recepie
            /// </summary>
            public bool Abandon { get; set; }

            public List<Image> RecepieImages = new List<Image>();
            public List<DescriptionProcess> DescriptionsList = new List<DescriptionProcess>();
            public List<Ingredient> IngredientsList = new List<Ingredient>();

            public Recepie() { }
        }

        #endregion

        #region Ingredient class
        public class Ingredient
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Ammount { get; set; }
            public string AmmountName { get; set; }

            public Ingredient() { }
        }
        #endregion
        #region DescriptionProcess class
        public class DescriptionProcess
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
            public double Time { get; set; }

            public DescriptionProcess() { }
        }
        #endregion
        #region Image class
        public class Image
        {
            public int Id { get; set; }
            public string Path { get; set; }
            public string Name { get; set; }

            public Image() { }
        }
        #endregion

        #region Rating class
        public class Rating
        {
            public int Id { get; set; }
            public int RatePoint { get; set; }
            public string Class { get; set; }

            public Rating() { }
        }
        #endregion

        #region Type class
        public class Type
        {
            public int Id { get; set; }
            public int TypeNumber { get; set; }
            public string Name { get; set; }
            public string Class { get; set; }

            public Type() { }
        }
        #endregion

        #region Functions and variables for toglleying websites
        protected bool main_page = true;
        protected bool create_recipe_page = false;
        protected bool display_recipe_page = false;
        protected bool display_recipe_edit_page = false;

        //Will show all recepies and filter them
        protected bool display_all_recipies = false;
        protected bool ongoing_recipies = false;
        protected bool finished_recipies = false;
        protected bool abandoned_recipies = false;

        /// <summary>
        /// Object for editing recipe
        /// </summary>
        protected Recepie edit_recipe_object = new Recepie();

        /// <summary>
        /// Will close all pages views
        /// </summary>
        public void CloseAllPages()
        {
            main_page = false;
            create_recipe_page = false;
            display_recipe_page = false;
            display_recipe_edit_page = false;
            display_all_recipies = false;

            abandoned_recipies = false;
            finished_recipies = false;
            ongoing_recipies = false;

            ScrollTop();
        }

        public void OpenOngoingRecipies()
        {
            CloseAllPages();
            ongoing_recipies = true;
            display_all_recipies = true;
        }

        public void OpenFinishedRecipies()
        {
            CloseAllPages();
            finished_recipies = true;
            display_all_recipies = true;
        }

        public void OpenAbandonRecipies()
        {
            CloseAllPages();
            abandoned_recipies = true;
            display_all_recipies = true;
        }

        /// <summary>
        /// Will open window with all recepies
        /// </summary>
        public void OpenAllRecepies()
        {
            CloseAllPages();
            display_all_recipies = true;
        }

        /// <summary>
        /// Will open main page
        /// </summary>
        public void OpenMainPage()
        {
            CloseAllPages();
            main_page = true;
        }

        /// <summary>
        /// Will open create recepie page
        /// </summary>
        public void OpenCreateRecipePage()
        {
            CloseAllPages();
            create_recipe_page = true;
        }

        /// <summary>
        /// Will open single recepie page
        /// </summary>
        public void OpenRecipePage(int Id)
        {
            CloseAllPages();
            current_recepie = Recepies[Id];

            if (current_recepie.RecepieImages.Count > 0)
            {
                current_recepie_image = current_recepie.RecepieImages[0];
            }

            display_recipe_page = true;
        }

        /// <summary>
        /// Will open edit reciep page
        /// </summary>
        public void OpenEditRecipe()
        {
            CloseAllPages();
            edit_type_base = "checked_rating";
            edit_rate_base = "checked_rating";
            edit_recipe_object = current_recepie;



            display_recipe_edit_page = true;
        }

        #endregion

        #region Create Recepie
        //Variables for creating recepie
        protected string new_recepie_name = "";
        protected string new_recepie_description = "";
        protected int creator_recepie_rating = 0;
        protected int user_rating = 0;
        protected int new_type;

        /// <summary>
        /// This object will store current recepie for display(single recepie div)
        /// </summary>
        protected Recepie current_recepie = new Recepie();

        /// <summary>
        /// Will showcase image of the current recepie in gallery
        /// </summary>
        protected Image current_recepie_image = new Image();

        /// <summary>
        /// This will contain all ingredients for new recepie | Max Count = 100
        /// </summary>
        protected List<Ingredient> ingredients = new List<Ingredient>();

        /// <summary>
        /// This will contain all steps to create recepie
        /// </summary>
        protected List<DescriptionProcess> steps = new List<DescriptionProcess>();

        /// <summary>
        /// Will hold images for the new recipe
        /// </summary>
        protected List<Image> images = new List<Image>();

        /// <summary>
        /// Will hold recepies 
        /// </summary>
        protected List<Recepie> Recepies = new List<Recepie>();

        /// <summary>
        /// This variable will hold recepies for example user in this demonstration
        /// </summary>
        protected UserRecepies example_user = new UserRecepies();

        /// <summary>
        /// This list will hold rating numbers form 1 to 10
        /// </summary>
        protected List<Rating> ratings = new List<Rating>();

        /// <summary>
        /// This List will store types of which food is
        /// </summary>
        protected List<Type> types = new List<Type>();

        /// <summary>
        /// Latest 6 ongoing recepies of the user displayed in main window
        /// </summary>
        protected List<Recepie> latest_6_ongoing_recepies = new List<Recepie>();

        /// <summary>
        /// Latest 3 finished recepies of the user displayed in main window
        /// </summary>
        protected List<Recepie> latest_3_finished_recepies = new List<Recepie>();

        /// <summary>
        /// Latest 6 abandoned recepies of the user displayed in main window
        /// </summary>
        protected List<Recepie> latest_6_abandoned_recepies = new List<Recepie>();

        /// <summary>
        /// Will reset variables for input
        /// </summary>
        public void ResetVariablesCreateNew()
        {
            types = new List<Type>();
            ratings = new List<Rating>();
            Recepies = new List<Recepie>();
            steps = new List<DescriptionProcess>();
            images = new List<Image>();
            ingredients = new List<Ingredient>();
            new_recepie_name = "";
            new_recepie_description = "";
            creator_recepie_rating = 0;
            user_rating = 0;
            new_type = 0;
        }

        /// <summary>
        /// Will add new window for adding ingredient
        /// </summary>
        public void AddNewIngredient()
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Id = ingredients.Count;
            ingredients.Add(ingredient);
        }


        /// <summary>
        /// Will add new window for adding next step
        /// </summary>
        public void AddNewStep()
        {
            DescriptionProcess descriptionProcess = new DescriptionProcess();
            descriptionProcess.Id = steps.Count;
            steps.Add(descriptionProcess);
        }

        /// <summary>
        /// Will add new window for adding new image
        /// </summary>
        public void AddNewImage()
        {
            Image i = new Image();
            i.Id = images.Count;
            images.Add(i);
        }

        /// <summary>
        /// Will change rating how user liked its recepie
        /// </summary>
        public void ChangeRating(int rating)
        {
            for (int i = 0; i < ratings.Count; i++)
            {
                ratings[i].Class = "";
            }

            user_rating = rating;
            ratings[rating - 1].Class = "checked_rating";
        }

        /// <summary>
        /// Will type of the food 
        /// </summary>
        public void ChangeType(int type)
        {
            for (int i = 0; i < types.Count; i++)
            {
                types[i].Class = "";
            }

            types[type].Class = "checked_rating";
            new_type = type;
        }

        /// <summary>
        /// Adds recepie to
        /// </summary>
        public void AddNewRecepie()
        {
            Recepie r = new Recepie();

            r.Id = example_user.Ongoing_Recepies.Count;
            r.Abandon = false;
            r.Description = new_recepie_description;
            r.DescriptionsList = steps;
            r.Finished = false;
            r.Name = new_recepie_name;
            r.Ongoing = true;
            r.Rating = creator_recepie_rating;
            r.RecepieImages = images;
            r.IngredientsList = ingredients;
            r.Rating = user_rating;
            r.Type = new_type;

            example_user.Ongoing_Recepies.Add(r);

            ResetVariablesCreateNew();
        }


        #endregion
        protected List<string> lines_recepies = new List<string>();


        protected override async Task OnInitializedAsync()
        {
            //Create first ingredient for window
            Ingredient ingredient = new Ingredient();
            ingredient.Id = 0;
            ingredients.Add(ingredient);

            //Create first step for window
            DescriptionProcess descriptionProcess = new DescriptionProcess();
            descriptionProcess.Id = 0;
            steps.Add(descriptionProcess);

            //Create first img for window
            Image img = new Image();
            img.Id = 0;
            images.Add(img);

            //Create ratings
            for (int i = 1; i < 11; i++)
            {
                Rating r = new Rating();
                r.Id = i - 1;
                r.RatePoint = i;
                ratings.Add(r);
            }

            Type t1 = new Type();
            Type t2 = new Type();
            Type t3 = new Type();
            Type t4 = new Type();
            Type t5 = new Type();

            t1.Id = 0;
            t2.Id = 1;
            t3.Id = 2;
            t4.Id = 3;
            t5.Id = 4;

            t1.Name = "Breakfast";
            t2.Name = "Lunch";
            t3.Name = "Dinner";
            t4.Name = "Dessert";
            t5.Name = "Snack";

            t1.TypeNumber = 0;
            t2.TypeNumber = 1;
            t3.TypeNumber = 2;
            t4.TypeNumber = 3;
            t5.TypeNumber = 4;

            types.Add(t1);
            types.Add(t2);
            types.Add(t3);
            types.Add(t4);
            types.Add(t5);

            //Load sample data

            //Load recepies objects
            string FilePath = Path.GetFullPath(@"wwwroot\Data\RecepiesData.txt");
            lines_recepies = File.ReadAllLines(FilePath).ToList();

            //Create instances
            int number_of_objects = lines_recepies.Count / 8;
            for(int i = 0; i < number_of_objects; i++)
            {
                Recepie r = new Recepie();
                r.Id = i;
                Recepies.Add(r);
            }

            //Fill instances with data
            int counter = 0;
            int id = 0;
            for (int i = 0; i < lines_recepies.Count; i++)
            {
                if(counter == 0)
                {
                    Recepies[id].Name = lines_recepies[i];
                }

                if(counter == 1)
                {
                    Recepies[id].Description = lines_recepies[i];
                }

                if(counter == 2)
                {
                    Recepies[id].Type = Convert.ToInt32(lines_recepies[i]);
                }

                if(counter == 3)
                {
                    Recepies[id].Rating = Convert.ToInt32(lines_recepies[i]);
                }

                if(counter == 4)
                {
                    if(lines_recepies[i] == "T")
                    {
                        Recepies[id].Abandon = true;
                    }
                    else
                    {
                        Recepies[id].Abandon = false;
                    }
                }

                if (counter == 5)
                {
                    if (lines_recepies[i] == "T")
                    {
                        Recepies[id].Finished = true;
                    }
                    else
                    {
                        Recepies[id].Finished = false;
                    }
                }

                if (counter == 6)
                {
                    if (lines_recepies[i] == "T")
                    {
                        Recepies[id].Ongoing = true;
                    }
                    else
                    {
                        Recepies[id].Ongoing = false;
                    }
                }

                if(counter == 7)
                {
                    id++;
                    counter = 0;
                }

                if(lines_recepies[i] != "END")
                {
                    counter++;
                }
            }

            //Load photos objects
            string FilePath2 = Path.GetFullPath(@"wwwroot\Data\ImagesFoodData.txt");
            List<string> lines_photos = new List<string>();
            lines_photos = File.ReadAllLines(FilePath2).ToList();

            id = 0;
            counter = 0;
            //Add Photos to the list
            for(int i = 0; i < lines_photos.Count; i++)
            {
                if(counter == 0 && lines_photos[i] != "END")
                {
                    Image image = new Image();
                    Recepies[id].RecepieImages.Add(image);
                    Recepies[id].RecepieImages[Recepies[id].RecepieImages.Count - 1].Id = Recepies[id].RecepieImages.Count - 1;
                    Recepies[id].RecepieImages[Recepies[id].RecepieImages.Count - 1].Path = lines_photos[i];
                }

                if(counter == 1 && lines_photos[i] != "END")
                {
                    Recepies[id].RecepieImages[Recepies[id].RecepieImages.Count - 1].Name = lines_photos[i];
                }


                if(lines_photos[i] != "END" && counter != 1)
                {
                    counter++;
                }
                else if (lines_photos[i] != "END" && counter == 1)
                {
                    counter = 0;
                }

                if (lines_photos[i] == "END")
                {
                    id++;
                }
            }

            //Load steps objects
            string FilePath3 = Path.GetFullPath(@"wwwroot\Data\StepsData.txt");
            List<string> lines_steps = new List<string>();
            lines_steps = File.ReadAllLines(FilePath3).ToList();

            id = 0;
            counter = 0;
            for(int i = 0; i < lines_steps.Count; i++)
            {
                if(lines_steps[i] != "END")
                {
                    DescriptionProcess d = new DescriptionProcess();
                    Recepies[id].DescriptionsList.Add(d);
                    Recepies[id].DescriptionsList[Recepies[id].DescriptionsList.Count - 1].Id = Recepies[id].DescriptionsList.Count - 1;
                    Recepies[id].DescriptionsList[Recepies[id].DescriptionsList.Count - 1].Text = lines_steps[i];
                }
                else
                {
                    id++;
                }
            }

            //Load ingrediends objects
            string FilePath4 = Path.GetFullPath(@"wwwroot\Data\IngredientsData.txt");
            List<string> lines_ingredients = new List<string>();
            lines_ingredients = File.ReadAllLines(FilePath4).ToList();

            id = 0;
            counter = 0;

            for(int i = 0; i < lines_ingredients.Count; i++)
            {
                if(counter == 0 && lines_ingredients[i] != "END")
                {
                    Ingredient ingr = new Ingredient();
                    Recepies[id].IngredientsList.Add(ingr);
                    Recepies[id].IngredientsList[Recepies[id].IngredientsList.Count - 1].Id = Recepies[id].IngredientsList.Count - 1;
                    Recepies[id].IngredientsList[Recepies[id].IngredientsList.Count - 1].Name = lines_ingredients[i];
                    
                }

                if(counter == 1 && lines_ingredients[i] != "END")
                {
                    Recepies[id].IngredientsList[Recepies[id].IngredientsList.Count - 1].Ammount = Convert.ToDouble(lines_ingredients[i]);
                }

                if(counter == 2 && lines_ingredients[i] != "END")
                {
                    Recepies[id].IngredientsList[Recepies[id].IngredientsList.Count - 1].AmmountName = lines_ingredients[i];
                }

                if(lines_ingredients[i] != "END" && counter != 2)
                {
                    counter++;

                }
                else if(lines_ingredients[i] != "END" && counter == 2)
                {
                    counter = 0;
                }

                if(lines_ingredients[i] == "END")
                {
                    id++;
                }
            }


            //Load 6 latest ongoing recepies
            counter = 0;
            if(Recepies.Count > 0)
            {
                for(int i = Recepies.Count; i != 0; i--)
                {
                    if (counter == 6)
                    {
                        break;
                    }

                    if (Recepies[i - 1].Ongoing == true)
                    {
                        latest_6_ongoing_recepies.Add(Recepies[i - 1]);
                        counter++;
                    }
                }
            }


            //Load 6 latest ababdoned recepies
            counter = 0;
            if (Recepies.Count > 0)
            {
                for (int i = Recepies.Count; i != 0; i--)
                {
                    if (counter == 6)
                    {
                        break;
                    }

                    if (Recepies[i - 1].Abandon == true)
                    {
                        latest_6_abandoned_recepies.Add(Recepies[i - 1]);
                        counter++;
                    }
                }
            }


            //Load 3 latest finished recepies
            counter = 0;
            if (Recepies.Count > 0)
            {
                for (int i = Recepies.Count; i != 0; i--)
                {
                    if (counter == 3)
                    {
                        break;
                    }

                    if (Recepies[i - 1].Finished == true)
                    {
                        latest_3_finished_recepies.Add(Recepies[i - 1]);
                        counter++;
                    }
                }
            }
        }


        #region Current Recepie Functions
        /// <summary>
        /// Will show next image in recepie gallery
        /// </summary>
        public void NextImage()
        {
            if(current_recepie.RecepieImages.Count > 0)
            {
                if(current_recepie_image.Id != current_recepie.RecepieImages.Count - 1)
                {
                    current_recepie_image = current_recepie.RecepieImages[current_recepie_image.Id + 1];
                }
                else
                {
                    current_recepie_image = current_recepie.RecepieImages[0];
                }
            }
        }


        /// <summary>
        /// Will show previous image in recepie gallery
        /// </summary>
        public void PreviousImage()
        {
            if (current_recepie.RecepieImages.Count > 0)
            {
                if (current_recepie_image.Id != 0)
                {
                    current_recepie_image = current_recepie.RecepieImages[current_recepie_image.Id + -1];
                }
                else
                {
                    current_recepie_image = current_recepie.RecepieImages[current_recepie.RecepieImages.Count - 1];
                }
            }
        }
        #endregion


        #region Editing functions
        //This object will apply class for checkbox of rating before edit
        protected string edit_rate_base = "checked_rating";
        protected string edit_type_base = "checked_rating";


        /// <summary>
        /// Will change rating how user liked its recepie
        /// </summary>
        public void EditChangeRating(int rating)
        {
            for (int i = 0; i < ratings.Count; i++)
            {
                ratings[i].Class = "";
            }
            edit_recipe_object.Rating = rating;
            ratings[rating - 1].Class = "checked_rating";
        }


        /// <summary>
        /// Will type of the food 
        /// </summary>
        public void EditChangeType(int type)
        {
            for (int i = 0; i < types.Count; i++)
            {
                types[i].Class = "";
            }
            edit_type_base = "";
            types[type].Class = "checked_rating";
            new_type = type;
        }

        #endregion

        #region Help Functions
        private async Task ScrollTop()
        {
            await JSRuntime.InvokeAsync<string>("ScrollTopJS");

        }
        #endregion


        #region navigation functions
        protected string nav_toggle = "";
        protected bool show_btn_nav = true;
        public void ToggleNav()
        {
            if (nav_toggle == "")
            {
                nav_toggle = "nav_container_toggled";
                show_btn_nav = false;
            }
            else
            {
                nav_toggle = "";
                show_btn_nav = true;
            }
        }
        #endregion
    }
}
