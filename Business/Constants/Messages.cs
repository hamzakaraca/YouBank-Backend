using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;

namespace Business.Constants
{
    public class Messages
    {
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string UserRegistered = "Kullanıcı kaydı başarılı";
        public static string AddedCostumer = "Müşteri ekleme başarılı";
        public static string AddedAccount = "Hesap ekleme başarılı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Giriş başarılı";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu.";
        public static string AuthorizationDenied = "Yetkiniz yok!!!";
        public static string AccountListed = "Hesaplar Listelendi";
    }
}
