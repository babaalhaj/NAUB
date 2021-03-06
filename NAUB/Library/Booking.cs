﻿using NAUB.Models;
using NAUB.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;

namespace NAUB.Library
{
    public static class Booking
        {
            public static void AddBooking(BorrowViewModel borrowViewModel, ApplicationDbContext context)
            {

                var books = context.Books.Where(b => borrowViewModel.MyBooks.Contains(b.Isbn)).ToList();
                short counter = 0;
                foreach (var book in books)
                {
                    book.NumberInStock--;

                    var borrows = new Borrow
                    {
                        BorrowerType = borrowViewModel.Borrow.BorrowerType,
                        BorrowerId = borrowViewModel.Borrow.BorrowerId,
                        Name = borrowViewModel.Borrow.Name,
                        Isbn = borrowViewModel.MyBooks[counter],
                        BorrowDate = DateTime.Today
                    };

                    var entry = context.Entry(borrows);
                    if (entry.State == EntityState.Detached)
                        context.Borrows.Attach(borrows);

                    counter++;
                    context.Borrows.Add(borrows);
                }

                context.SaveChanges();
            }
        }
    }