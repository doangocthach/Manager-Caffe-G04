// using System;

// namespace Persitence.Model
// {
//     class OrderDetail
//     {
//         public OrderDetail(int orderID, int itemID, int itemCount)
//         {
//             OrderID = orderID;
//             ItemID = itemID;
//             ItemCount = itemCount;
//         }
 
//         public override bool Equals(object obj)
//         {

//             OrderDetail orderDetail = (OrderDetail)obj;
//             return OrderID == orderDetail.OrderID;

//         }

//         // override object.GetHashCode
//         public override int GetHashCode()
//         {

//             return (OrderID + ItemID + ItemCount ); 
//         }
//         public OrderDetail()
//         { }
//         public int OrderID { get; set; }
//         public int ItemID { get; set; }

//         public int ItemCount { get; set; }
//     }

// }