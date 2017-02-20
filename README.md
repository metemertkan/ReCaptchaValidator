# ReCaptchaValidator

reCAPTCHA V2 validator.

https://www.nuget.org/packages/SmyrnaReCaptchaValidator/

https://www.google.com/recaptcha/intro/index.html

How to use it via Action Filter
-------------------------------
1-Add following line into web.config
```html
<configuration>
  <appSettings>
    <add key="reCaptcha_privateKey" value="your_private_key" />
  </appSettings>
</configuration>
```
2-Create a view such as following
```html
<script src='https://www.google.com/recaptcha/api.js'></script>

<form action="/Home/TestRecaptcha" method="POST">
    <div class="g-recaptcha" data-sitekey="your_site_key"></div>
    <br/>
    <input type="submit" value="Submit">
</form>
```
All you need to do is defining the ```[ValidateReCaptcha] ``` as an action attribute and check ```ModelState``` in the controller
```cs
[HttpPost]
[ValidateReCaptcha]
public ActionResult TestRecaptcha()
{
  if (ModelState.IsValid)
    {
      return RedirectToAction("Index");
    }
    return RedirectToAction("Error");
}
```
