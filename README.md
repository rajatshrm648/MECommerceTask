Please refer this link below if you want to see the details implementatios with Images.
-----------------------------------------------------------------------------------------------
https://docs.google.com/document/d/1RwokjDKxiEEeX40CcIIYCJliU5Vt2UezWQZLOv-83xw/edit?usp=sharing
-------------------------------------------------------------------------------------------------
# MECommerceTask
Task for Monk Commerce
Readme File
Hi MonkEcommerce,
I am Rajat Sharma, I had completed the assignment which was given through mail.
Here are the following details which i had implemented , some scenarios and the 
Case which i had not implemented.
I had created Api’s in .NET as I have hands on experience in this language
and used SQLite for database.
let's quickly discuss the Database Schema.
Following are the tables which i created
1. Coupons
2. CartWiseCoupons
3. Products
4. ProductWiseCoupon
for more detail you can refer the MeCommerceTask.db

Following are the API's in details specification and input case.

I have created differentControllers like CartController, CouponController, ProductWiseController
lets discuss
1. CouponController:
-> /GetCoupons
By just Simple Execution you can get the list of all coupons.In Coupons tables
i had mentioned proper coupon_id , type, discountValueInPercent, expirationDate
I created the seperate table for Coupons, For further implementation of new coupons in
future.
 

2. /GetCoupon/{id}
By using this API , you can get the specific coupon using coupon_id from Coupons table. In the code, I properly handled the case also If there is no coupon found so just return a simple message and if any error occurred so that also handled in try/catch, I did this same majorly in all APIs.
Input Case:
Input_1 = 1                                                  Input_2 = 2				Input_3 =  3
 


3. /CreateCoupon
This Api can insert new Coupons as when we required further in the future.
Input:
{
  "id": 4, 
  "type": "MAXCODE",
  "discountValueInPercent": 50,
  "expirationDate": "2026-03-21T19:14:10.418Z"
}


 
 

The case here I haven’t implemented here is if I put the type “” empty string then also I will store it into the database so that’s wrong here.
 
 

4. /UpdateCoupon/{id}
I mentioned to fill the id required to update the what ever you want to do update in Coupons.
Input
Id : 4
{
  "id": 4,
  "type": "SHOWTIME",
  "discountValueInPercent": 40,
  "expirationDate": "2026-03-21T19:24:48.750Z"}
 

5.  /DeleteCoupon/{id}
we using this api we can delete the any coupon which we want using ID.
--------------------------------------------------------------------------------------------------------
CART CONTROLLER API’s
1.	/ applicableCouponsCartWise
 By using this api we can get the list of all coupons which are eligible for a specific cart it depends on the totalPrice of the cart. In the table CartWiseCoupon we have the maintain the threshold for CouponId’s , if total price exceeds the threshold then the coupons are eligible.
Input Case: 1
{
  "totalPrice": 150,
  "items": []
}
Input Case: 2
{
  "totalPrice": 99,
  "items": []
}
Output of Case 1:
 


Limitaions of applicableCouponsCartWise
Api does not check if the coupon is expired, If a coupon is one-time use only, this api does not prevent.


2.	/applicableCouponsCartWise/{id}
This api only retrieves coupons that can be applied. Applies Cart -wise coupon to cart and calculates the final discounted price.

Input Case 1
id = 1
{ 
  "totalPrice": 200,
  "finalPrice": 200 
}
 
 

Limitations:
If coupon is expired it still applies, api applies the coupon but doesn’t store it. So user can apply it multiple times.



ProductWise Controller

-> /ProductWise
We stored the product in the Product table and we send productid in the Input Parameter. It fetch the product with product Wise Coupon and give the disount Amount and the FinalPrice and it returns
InputCase 1:
productid = 5
 
Limitaions:
Api apply the coupon but doesn’t store it, so same coupon can apply multiple times, if coupon is expired , it still applies.

BxGy Coupon Controller:
Sorry, I haven’t created any api related to this , due to less time and I thought I already took the extra time . If you are satisfied with my above api , I can also create api for this coupon also.
-------------------------------------------------------------------------------------------------------------

So in above all api , I handled different logics , applied CRUD operations, provide limitations and I can improve It more by involving more scenerios.
I hope , I had design the API as per you expectations. Let me know your feedback. 
Looking forward to here you from your side.

You can refer my code as mentioned on GitHub.

Thanks 
Rajat Sharma

