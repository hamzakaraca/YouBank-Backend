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
        public static string UserListed = "Kullanıcılar Listelendi";
        public static string PasswordUpdated = "Şifre başarıyla güncellendi";
        public static string NewPasswordsMatchError = "Yeni şifreler eşleşmiyor";
        public static string PasswordsSame="Eski şifre ile yeni şifre aynı olamaz";
        public static string PersonNotFound = "Kimlik bilgileriniz yanlış";
        public static string NationalityNumberAlreadyExist="Sistemde bu tc nosuna sahip kullanıcı mevcut";
        public static string AccountUpdated = "Hesap güncellendi";
        public static string AddMoneySuccess="Para başarıyla hesabınıza yatırıldı.";
        public static string InsufficientMoney="Hesaptaki para yetersiz.";
        public static string IncorrectQuantity="Geçersiz miktar girdiniz.Lütfen pozitif değer giriniz";
    }
}
