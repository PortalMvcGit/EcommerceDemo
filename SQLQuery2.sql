
Select * from Product P 
left Join ProductAttribute PA 
on P.ProductId = PA.ProductId
left Join ProductAttributeLookup PL
On PL.AttributeId = PA.AttributeId
where p.ProductId=1


---Drop Down : Car : 1| Mobile : 2
---Based on Selection Show different tax box
---ProdName,ProdDescription,1 -- [Color,Make,Model]
--OR 
---ProdName,ProdDescription,2 -- [RAM,Front Camera,Rear Camera]


Select pl.ProdCatId,pc.CategoryName,Count(pl.ProdCatId) as TotalCategory 
from ProductAttributeLookup pl inner join ProductCategory pc
on pc.ProdCatId = pl.ProdCatId
group by pl.ProdCatId,pc.CategoryName
