﻿using Strategy_Pattern_First_Look.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern.Business.Strategies.Invoice
{
    public class EmailInvoiceStrategy : InvoiceStrategy
    {
        public override void Generate(Order order)
        {
            using SmtpClient client = new("smtp.sendgrid.net", 587);
            NetworkCredential credentials = new("USERNAME", "PASSWORD");
            client.Credentials = credentials;

            MailMessage mail = new("Hemant Koti", "kotihemant5@gmail.com")
            {
                Subject = "We've created an invoice for your order",
                Body = GenerateTextInvoice(order)
            };

            client.Send(mail);
        }
    }
}
