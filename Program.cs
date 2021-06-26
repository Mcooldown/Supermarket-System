using CSharp_VincentHadinata_2301850430.Model;
using CSharp_VincentHadinata_2301850430.Repositories;
using System;
using System.Collections.Generic;

namespace CSharp_VincentHadinata_2301850430
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        int count_trans = 1;
        public Program()
        {
            int menu = -1;

            do
            {
                Console.WriteLine("Supermarket System");
                Console.WriteLine("========================");
                Console.WriteLine("1. Login as User");
                Console.WriteLine("2. Login as Admin");
                Console.WriteLine("3. Exit");
                Console.Write("Choice: ");

                menu = Convert.ToInt32(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        userMenu();
                        break;
                    case 2:
                        adminMenu();
                        break;
                }

            } while (menu != 3);
        }
        private void userMenu()
        {
            int menu = -1;
            do
            {
                Console.WriteLine("Supermarket System - User");
                Console.WriteLine("========================");
                Console.WriteLine("1. View Product");
                Console.WriteLine("2. Buy Product");
                Console.WriteLine("3. Exit");
                Console.Write("Choice: ");

                menu = Convert.ToInt32(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        viewProduct();
                        break;
                    case 2:
                        buyProduct();
                        break;
                }

            } while (menu != 3);
        }

        private void viewProduct()
        {
            List<Product> listProduct = ProductRepository.view();

            Console.WriteLine("List Product");
            Console.WriteLine("===================");

            if (listProduct.Count == 0)
            {
                Console.WriteLine("No Product available");
            }
            else
            {
                foreach (Product product in listProduct)
                {
                    Console.WriteLine("Product ID       : " + product.id);
                    Console.WriteLine("Product Name     : " + product.name);
                    Console.WriteLine("Product Quantity : " + product.quantity);
                    Console.WriteLine("Price            : " + product.price);
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void buyProduct()
        {
            List<Product> listProduct = ProductRepository.view();

            if (listProduct.Count <= 0)
            {
                Console.WriteLine("No product available");
            }
            else
            {
                Console.WriteLine("Buy Product");
                Console.WriteLine("===================");

                string again = null;
                int count_buy = 0;
                DetailTransaction detail = new DetailTransaction();
                do
                {
                    do
                    {
                        Console.Write("Input Product ID [1-" + listProduct.Count + "]: ");
                        detail.prod_id = Convert.ToInt32(Console.ReadLine());

                    } while (detail.prod_id < 1 || detail.prod_id > listProduct.Count);

                    Product product = ProductRepository.search(detail.prod_id, 1);

                    if (product.quantity > 0)
                    {
                        do
                        {
                            Console.Write("Input Product Quantity [1-" + product.quantity + "]: ");
                            detail.quantity = Convert.ToInt32(Console.ReadLine());

                        } while (detail.quantity < 1 || detail.quantity > product.quantity);


                        if (count_buy == 0)
                        {
                            detail.tr_id = DetailTransactionRepository.insert(detail, 1);
                            count_buy = 1;
                        }
                        else detail.tr_id = DetailTransactionRepository.insert(detail, 2);
                    }
                    else
                    {
                        Console.WriteLine("Product sold out");
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadKey();
                    }

                    do
                    {
                        Console.WriteLine("Do you want to add another product? [Yes|No]");
                        again = Console.ReadLine();

                    } while (!again.Equals("Yes") && !again.Equals("No"));

                } while (again.Equals("Yes"));

                if(count_buy > 0)
                {
                    string method = null;
                    do
                    {
                        Console.Write("Choose payment method [Cash | Credit]: ");
                        method = Console.ReadLine();

                    } while (!method.Equals("Cash") && !method.Equals("Credit"));

                    int total_payment = DetailTransactionRepository.totalPayment(detail.tr_id, method);

                    Console.WriteLine("Rp " + total_payment + " Successfully paid by " + method);
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadKey();
                }
            }
        }

        private void adminMenu()
        {
            int menu = -1;
            do
            {
                Console.WriteLine("Supermarket System - Admin");
                Console.WriteLine("========================");
                Console.WriteLine("1. Insert Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. View Product");
                Console.WriteLine("5. View Transaction");
                Console.WriteLine("6. Exit");
                Console.Write("Choice: ");

                menu = Convert.ToInt32(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        insertProduct();
                        break;
                    case 2:
                        updateProduct();
                        break;
                    case 3:
                        deleteProduct();
                        break;
                    case 4:
                        viewProduct();
                        break;
                    case 5:
                        viewTransaction();
                        break;
                }

            } while (menu != 6);
        }

        private void insertProduct()
        {
            Product product = new Product();
            Console.WriteLine("Insert Product");
            Console.WriteLine("==================");
            do
            {
                Console.Write("Input product name [length between 5-20]: ");
                product.name = Console.ReadLine();

            } while (product.name.Length < 5 || product.name.Length > 20);

            do
            {
                Console.Write("Input product price [1000- 1000000]: ");
                product.price = Convert.ToInt32(Console.ReadLine());

            } while (product.price < 1000 || product.price > 1000000);

            do
            {
                Console.Write("Input product quantity [1-1000]: ");
                product.quantity = Convert.ToInt32(Console.ReadLine());

            } while (product.quantity < 1 || product.quantity > 1000);

            ProductRepository.insert(product);

            Console.WriteLine("The product has been successfully inserted!");
            Console.Write("Press enter to continue...");
            Console.ReadKey();
        }

        private void updateProduct()
        {
            List<Product> listProduct = ProductRepository.view();
            Console.WriteLine("Update Product");
            Console.WriteLine("==================");

            if (listProduct.Count <= 0)
            {
                Console.WriteLine("No product available");
            }
            else
            {
                Product product = new Product();
                do
                {
                    Console.Write("Input Product ID [1-" + listProduct.Count + "]: ");
                    product.id = Convert.ToInt32(Console.ReadLine());

                } while (product.id < 1 || product.id > listProduct.Count);

                do
                {
                    Console.Write("Input product name [length between 5-20]: ");
                    product.name = Console.ReadLine();

                } while (product.name.Length < 5 || product.name.Length > 20);

                do
                {
                    Console.Write("Input product price [1000- 1000000]: ");
                    product.price = Convert.ToInt32(Console.ReadLine());

                } while (product.price < 1000 || product.price > 1000000);

                do
                {
                    Console.Write("Input product quantity [1-1000]: ");
                    product.quantity = Convert.ToInt32(Console.ReadLine());

                } while (product.quantity < 1 || product.quantity > 1000);

                ProductRepository.update(product);

                Console.WriteLine("The product has been successfully updated!");
            }
            Console.Write("Press enter to continue...");
            Console.ReadKey();
        }

        private void deleteProduct()
        {
            List<Product> listProduct = ProductRepository.view();
            Console.WriteLine("Delete Product");
            Console.WriteLine("==================");

            if (listProduct.Count <= 0)
            {
                Console.WriteLine("No Product Available");
            }
            else
            {
                int prod_id = 0;
                do
                {
                    Console.Write("Input Product ID [1-" + listProduct.Count + "]: ");
                    prod_id = Convert.ToInt32(Console.ReadLine());

                } while (prod_id < 1 || prod_id > listProduct.Count);

                Product delete = ProductRepository.search(prod_id, 1);

                Console.WriteLine("");
                Console.WriteLine("Product ID       : " + delete.id);
                Console.WriteLine("Product Name     : " + delete.name);
                Console.WriteLine("Product Quantity : " + delete.quantity);
                Console.WriteLine("Price            : " + delete.price);
                Console.WriteLine("");

                string deletenot = null;
                do
                {
                    Console.Write("Are you sure want to delete this product? [Yes|No]: ");
                    deletenot = Console.ReadLine();

                } while (!deletenot.Equals("Yes") && !deletenot.Equals("No"));

                if (deletenot.Equals("Yes"))
                {
                    ProductRepository.delete(delete.id);
                    Console.WriteLine("The product has been successfully deleted");
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadKey();
                }
            }
        }

        private void viewTransaction()
        {
            List<HeaderTransaction> list_header = HeaderTransactionRepository.viewTransaction_header();

            if (list_header.Count <= 0)
            {
                Console.WriteLine("No Transaction");
            }
            else
            {
                Console.WriteLine("View Transaction");
                Console.WriteLine("=====================");
                foreach (HeaderTransaction header in list_header)
                {
                    Console.WriteLine("Transaction ID   : " + header.tr_id);
                    List<Product> list_detail = DetailTransactionRepository.viewTransactionDetail(header.tr_id);

                    int count = 1;
                    Console.WriteLine("|No  |Product Name        |Quantity |Price     |");
                    foreach (Product detail in list_detail)
                    {
                        Console.WriteLine("|{0,-4}|{1,-20}|{2,-9}|{3,-10}|", count, detail.name, detail.quantity, detail.price);
                        count++;
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Grand Total: " + header.total + " by " + header.method);
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("Press enter to continue...");
            Console.ReadKey();
        }
    }
}
