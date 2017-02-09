#QQ空间相册下载器

##主要功能  

>批量下载指定QQ空间相册的所有图片

##实现方法：

1. 检测是否拥有目标QQ空间访问权限，若无则使用内嵌浏览器登录并获取cookie（以下操作均为携带cookie操作）  

2. 使用QQ空间开放API获取相册ID  
> http://photo.qq.com/fcgi-bin/fcg_list_album?uin=QQ号码  

3. 根据相册ID获取相册内容详细信息
> http://photo.qq.com/fcgi-bin/fcg_list_photo?uin=QQ号码&albumid=相册ID   

4. 根据返回的XML信息解析并选取照片

* 后期可根据 QQ互联API 添加相册的个性化批量管理等功能
