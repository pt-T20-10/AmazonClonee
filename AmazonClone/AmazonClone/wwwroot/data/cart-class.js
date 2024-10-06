class Cart{
  cartItems;
  #localStorageKey;

  constructor(localStorageKey){
     this.#localStorageKey = localStorageKey;
     this.#loadFromStorage(); 

  }
  #loadFromStorage(){

      this.cartItems = JSON.parse(localStorage.getItem(this.#localStorageKey));
    
      if(!this.cartItems){
    
        this.cartItems=[{
      productId: 
            'e43638ce-6aa0-4b85-b27f-e1d07eb678c6',
      quantity:2,      
      deliveryOptionId:'1'
    },
    {
      productId: 
            '15b6fc6f-327a-4ec4-896f-486349e85a3d',
      quantity : 1,
      deliveryOptionId:'2'
    }];
      }}
  savetoStorage(){
      localStorage.setItem(this.#localStorageKey,JSON.stringify(this.cartItems));
    } 
  addToCart(productId){
        let matchingItem;
        this.cartItems.forEach((Cartitem) => {
          if( productId === Cartitem.productId){
              matchingItem = Cartitem;
          }
        }); 
      
        if(matchingItem){
          matchingItem.quantity += 1;
        } else {
            this.cartItems.push({
              productId: productId,
              quantity: 1,
              deliveryOptionId:''
        });
      }
      this.savetoStorage();
      }
  removeFromCart(productId){
        const newCart =[];
        this.cartItems.forEach((Cartitem) =>{
          if( Cartitem.productId !== productId ){
            newCart.push(Cartitem);
          }
        });
        this.cartItems = newCart;
      
        this.savetoStorage();
      }  
  calculateCartQuantity() {
        let cartQuantity = 0;
      
        this.cartItems.forEach((cartItem) => {
          cartQuantity += cartItem.quantity;
        })
        return cartQuantity;
      }
  updateQuantity(productId,newQuantity){
        let matchingItem ={};
        this.cartItems.forEach((Cartitem) => {
        if( productId === Cartitem.productId){
            matchingItem = Cartitem;
           
        } matchingItem.quantity = newQuantity;
        
      }); 
      this.savetoStorage();
    }
  updateDeliveryOption(productId, deliveryOptionId){
      let matchingItem;
      this.cartItems.forEach((Cartitem) => {
        if( productId === Cartitem.productId){
            matchingItem = Cartitem;
        }
      });
       matchingItem.deliveryOptionId = deliveryOptionId ;
    
      this.savetoStorage();
    } 
  }


 export const cart = new Cart('cart-oop');

