﻿
using System;
using System.Collections.Generic;
using InvoiceManagementSystem.Database;
using InvoiceManagementSystem.Validations;




namespace InvoiceManagementSystem.Controllers
{
    public class ProductController:BaseEntity
    {
        public void ProductSelection()
        {
            bool exit = false;

            while (!exit)
            {
            
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Delete Product");
                Console.WriteLine("3. Edit Product");
                Console.WriteLine("4. Display Product");
                Console.WriteLine("5. Back");
                Console.Write("Select an option: ");

                int option;
                var ProductRepoObject = new Repositories.ProductRepository();
                var ProductObject = new Models.Product();
                try
                {


                    if (int.TryParse(Console.ReadLine(), out option))
                    {
                        switch (option)
                        {
                            case 1:


                                Console.Write("Enter the product: ");
                                string ProductName = Console.ReadLine();
                                Console.Write("Enter the product description: ");
                                string ProductDescription = Console.ReadLine();
                                Console.Write("Enter the product price: ");
                                int ProductPrice = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter the Tax for the Product: ");
                                float ProductTax = float.Parse(Console.ReadLine());
                                Console.Write("Enter the Product discount: ");
                                double discount = Convert.ToDouble(Console.ReadLine());
                                float ProductDiscount =(float) discount / 100;
                                Console.WriteLine("Enter the Category ID");
                                string productCategoryiD = Console.ReadLine();

                                if (Category.CheckAvailability(productCategoryiD))
                                {
                                    var AddProduct = new Services.ProductService(ProductRepoObject);
                                    AddProduct.AddProductsService(ProductName, ProductDescription, ProductPrice, ProductDiscount, ProductTax, productCategoryiD);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Category ID");
                                    break;
                                }
                                break;
                            case 2:
                                Console.Write("Enter the product ID to delete: ");
                                string productID = Console.ReadLine();
                                if (ProductValidation.ProductIdValidation(productID))
                                {
                                    var DeleteProduct = new Services.ProductService(ProductRepoObject);
                                    DeleteProduct.DeleteProductsService(productID);
                                }
                                else
                                {
                                    Console.WriteLine("Entered ProductID is not valid");
                                }
                                break;
                            case 3:
                                Console.Write("Enter the Product ID to update: ");
                                string productId = Console.ReadLine();

                                foreach (var Product in EntityCollection.ProductList)
                                {
                                    if (Product.ProductID == productId)
                                    {
                                        Console.WriteLine("\nCategory ID: " + Product.ProductID);
                                        Console.WriteLine("Name: " + Product.ProductName);
                                        Console.WriteLine("Description: " + Product.ProductDescription);
                                        Console.WriteLine("Price: " + Product.ProductPrice);
                                        Console.WriteLine("Discount: " + Product.ProductDiscount);
                                        Console.WriteLine("Tax" + Product.ProductTax);
                                        Console.WriteLine("Category ID: " + Product.CategoryID);

                                        Console.WriteLine("\nSelect field to update: \n1. Name\n2. Description\n3. Price\n4. Discount\n5. Tax\n6. Back");
                                        Console.Write("\nEnter your choice: ");
                                        int editChoice = Convert.ToInt32(Console.ReadLine());
                                        Console.WriteLine("\n");
                                        switch (editChoice)
                                        {
                                            case 1:
                                                Console.Write("Enter new Product name: ");
                                                string productName = Console.ReadLine();
                                            
                                                dynamic EditProductName = new Services.ProductService(ProductRepoObject);
                                                EditProductName.EditProductDetailsService(Product,productName,Product.ProductDescription,Product.ProductPrice,Product.ProductDiscount,Product.ProductTax);
                                                Console.Write("\nProduct name updated successfully!!");
                                                break;
                                            case 2:
                                                Console.Write("Enter new Product description: ");
                                                dynamic productDescription = Console.ReadLine();
                                                dynamic EditProductDescription = new Services.ProductService(ProductRepoObject);
                                                EditProductDescription.EditProductDetailsService(Product,Product.ProductName,productDescription, Product.ProductPrice, Product.ProductDiscount, Product.ProductTax);
                                                Console.Write("\nProduct description updated successfully!!");
                                                break;
                                            case 3:
                                                Console.Write("Enter new Product price: ");
                                                dynamic productPrice = Convert.ToInt16(Console.ReadLine());
                                                dynamic EditProductPrice = new Services.ProductService(ProductRepoObject);
                                                EditProductPrice.EditProductDetailsService(Product, Product.ProductName, Product.ProductDescription, productPrice, Product.ProductDiscount, Product.ProductTax);
                                                Console.Write("\nProduct tax updated successfully!!");
                                                break;
                                            case 4:
                                                Console.Write("Enter new Product Discount: ");
                                                double Pdiscount = Convert.ToDouble(Console.ReadLine());
                                                double productDiscount = Pdiscount / 100;

                                                dynamic EditProductDiscount = new Services.ProductService(ProductRepoObject);
                                                EditProductDiscount(Product, Product.ProductName, Product.ProductDescription, Product.ProductPrice, productDiscount, Product.ProductTax);
                                                Console.Write("\nProduct tax updated successfully!!");
                                                break;
                                            case 5:
                                                Console.Write("Enter new Product Tax: ");
                                                dynamic productTax = float.Parse(Console.ReadLine());
                                                dynamic EditProductTax = new Services.ProductService(ProductRepoObject);
                                                EditProductTax.EditProductDetailsService(Product, Product.ProductName, Product.ProductDescription, Product.ProductPrice, Product.ProductDiscount, productTax);
                                                Console.Write("\nProduct tax updated successfully!!");
                                                break;
                                            case 6:
                                                break;
                                            default:
                                                Console.WriteLine("Invalid choice!!");
                                                break;
                                        }
                                        break;
                                    }

                                }

                                break;
                            case 4:
                                var DisplayProduct = new Services.ProductService(ProductRepoObject);
                                List<Models.Product> result = DisplayProduct.DisplayProductsService();
                                if(result.Count == 0)
                                {
                                    Console.WriteLine("Product List is Empty");
                                    break;
                                }
                                foreach (var Product in result)
                                {
                                    Console.Write($"ProductID          : {Product.ProductID}");
                                    Console.Write($"Product Name       : {Product.ProductName}");
                                    Console.Write($"Product Description: {Product.ProductDescription}");
                                    Console.Write($"Product Price      : {Product.ProductPrice}");
                                    Console.Write($"Product Discount   : {Product.ProductDiscount} ");
                                    Console.Write($"Product Tax        :{Product.ProductTax}");
                                    Console.Write($"Product CategoryID : {Product.CategoryID}");

                                }
                                break;
                            case 5:
                                exit = true;
                               
                                break;
                            default:
                                Console.WriteLine("Invalid option. Please try again.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please enter a number.");
                    }
                }catch {
                    DisplayMessage.DisplayErrorMessage("Input should be a number");
                }

               

            }
        }
    }
}

