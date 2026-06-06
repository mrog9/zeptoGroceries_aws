import { test, expect } from '@playwright/test';

test("has btn", async ({page}) => {

  // const browser = await chromium.launch();

  // const page = await browser.newPage();

  await page.goto('http://nginx:80');

  const username = "matt1234"


  const createLink = page.getByRole('link', {name: "Create one"})
  await expect(createLink).toBeVisible();
  await createLink.click();

  await expect(page).toHaveURL(new URLPattern({pathname: '*/signup*'}));

  const createBtn = await page.getByRole('button', {name: "Create Account"});
  await expect(createBtn).toBeVisible();

  const createInput = await page.getByPlaceholder('Choose a username', {exact:true});
  await expect(createInput).toBeVisible();
  await createInput.fill(username);
  await createBtn.click();

  await expect(page.getByText("Your account is now active!")).toBeVisible({
    timeout: 10000
  });

  const body = await page.locator("body").innerText();

  console.log(body);

  const loginLink = await page.getByRole("link");
  await loginLink.click()

  const loginBtn = await page.getByRole('button', {name: "Login"});
  await expect(loginBtn).toBeVisible();

  const signinInput = await page.getByPlaceholder('Enter your username', {exact:true});
  await expect(signinInput).toBeVisible();
  await signinInput.fill(username);
  await loginBtn.click();

  await expect(page).toHaveURL(new URLPattern({pathname: '*/searchProducts*'}));

  const headerTag = await page.getByRole('heading').filter({hasText: username});
  await expect(headerTag).toBeVisible();
})