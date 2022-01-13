using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkpoint_2
{
    public class ViewRemoveProduct
    {
        public static void removeArticle()
        {
            bool runRemoveProduct = true;

            while (runRemoveProduct)
            { 
                Console.Clear();

                Console.Write("Ange artikelnummer på artikeln du vill raddera (q = avbryt)? ");

                string menuOption = Console.ReadLine().Trim().ToLower();

                if (menuOption == "q")
                {
                    return;
                }

                if (typeof(MastersettingsArticle).GetType().GetProperty("format") != null)
                {
                    menuOption = CheckSyntax.check("Articles", menuOption.ToUpper()).fixedString;

                    if (CheckArticleSKU.checkArticleSKU(menuOption.ToUpper(), false))
                    {
                        var results = Program.articles.Find(str => str.articleSKU == menuOption);

                        if (results != null)
                        {
                            while (true)
                            {
                                Console.Write($"Vill du verkligen raddera artikeln {results.articleName} (J/n/q)? ");
                                string inputChoiceRemove = Console.ReadLine().Trim().ToLower();

                                if (inputChoiceRemove == "" || inputChoiceRemove == "j")
                                {
                                    RemoveArticle.removeArticle(menuOption.ToUpper());

                                    Console.WriteLine("Produkten är raderad! Tryck på en tangent!");
                                    Console.ReadKey();

                                    break;
                                }
                                else if (inputChoiceRemove == "n")
                                {
                                    break;
                                }
                                else if (inputChoiceRemove == "q")
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            while (true)
                            {
                                Console.Write("Produkten finns inte! Vill du se alla artiklar (J/n/q)?");
                                string inputShowArticles = Console.ReadLine().Trim().ToLower();

                                if (inputShowArticles == "" || inputShowArticles == "j")
                                {
                                    ShowArticles.showArticles();

                                    break;
                                }
                                else if (inputShowArticles == "n")
                                {
                                    break;
                                }
                                else if (inputShowArticles == "q")
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Produkten finns inte! Tryck på en tangent!");
                        Console.ReadKey();
                    }
                }
                
            
                else if (MastersettingsArticle.forceUpercase)
                {
                    if (CheckArticleSKU.checkArticleSKU(menuOption.ToUpper(), false))
                    {
                        var results = Program.articles.Find(str => str.articleSKU == menuOption.ToUpper());

                        if (results != null)
                        { 
                            while (true)
                            {
                                Console.Write($"Vill du verkligen raddera artikeln {results.articleName} (J/n/q)? ");
                                string inputChoiceRemove = Console.ReadLine().Trim().ToLower();

                                if (inputChoiceRemove == "" || inputChoiceRemove == "j")
                                {
                                    RemoveArticle.removeArticle(menuOption.ToUpper());

                                    Console.WriteLine("Produkten är raderad! Tryck på en tangent!");
                                    Console.ReadKey();

                                    break;
                                }
                                else if (inputChoiceRemove == "n")
                                {
                                    break;
                                }
                                else if (inputChoiceRemove == "q")
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            while (true)
                            {
                                Console.Write("Produkten finns inte! Vill du se alla artiklar (J/n/q)?");
                                string inputShowArticles = Console.ReadLine().Trim().ToLower();

                                if (inputShowArticles == "" || inputShowArticles == "j")
                                {
                                    ShowArticles.showArticles();

                                    break;
                                }
                                else if (inputShowArticles == "n")
                                {
                                    break;
                                }
                                else if (inputShowArticles == "q")
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Produkten finns inte! Tryck på en tangent!");
                        Console.ReadKey();
                    }
                }
                else
                {
                    if (CheckArticleSKU.checkArticleSKU(menuOption, false))
                    {
                        var results = Program.articles.Find(str => str.articleSKU == menuOption);

                        while (true)
                        {
                            Console.WriteLine($"Vill du verkligen raddera artikeln {results.articleName} (J/n/q)? ");
                            string inputChoiceRemove = Console.ReadLine().Trim().ToLower();

                            if (inputChoiceRemove == "" || inputChoiceRemove == "j")
                            {
                                RemoveArticle.removeArticle(menuOption.ToUpper());

                                Console.WriteLine("Produkten är raderad! Tryck på tangent!");
                                Console.ReadKey();

                                break;
                            }
                            else if (inputChoiceRemove == "n")
                            {
                                break;
                            }
                            else if (inputChoiceRemove == "q")
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Produkten finns inte! Tryck på tangent!");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
