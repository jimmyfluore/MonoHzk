# MonoHzk
支持MonoGame平台上简体中文（GB2312字符集）汉字的显示
## 简介：
点阵字体也叫位图字体，其中每个字形都以一组二维像素信息表示。这种文字显示方式于DOS操作系统时代被普遍采用，现在的性能非常有限的嵌入式设备上也有广泛应用。<br>
点阵字体采用二值化的表达方式，比直接采用8位或32位色深的贴图要大幅节约空间，但是，字体放大或非整数倍缩小时显示效果很差。<br>
在MonoGame平台上，这是目前自由度最高的使用汉字字体的方式，你可以在程序中自由使用GB2312字符集中的所有汉字，而不是固定文字内容的贴图。<br>
## 使用方法：
第一步，使用此项目中的HzkExporter，一个功能很有限但是还好用的导出点阵字体的工具，导出GB2312汉字点阵字体和英文（Asc32-127）点阵字体，是.bin格式，这种格式和经典的hzk、asc点阵字体是兼容的。程序中提供了若干已导出的汉字字体和英文字体。<br>
<br>
第二步，使用MonoGame的PipelineTool导入点阵字体。新建一个MonoGame Content Build Project，在Project栏里，点击Content图标，在Properties栏里点击References，引用MonoHzk.ContentPipeline.dll。这时PipelineTool即可支持MonoHzk的.bin格式点阵字体文件了。<br>
<br>
在Content中Add Existed Item，选择刚才导出的点阵字体(.bin)文件，Properties栏里Build Action选择Build，Importer选择MonoHzk Bin Importer，Processor选择MonoHzk Processor。最后Build即可。可参考HzkGame中Content.mgcb的配置。<br>
第三步，在程序中使用hzk。参考HzkGame中的用法即可。<br>
<br>
## QQ讨论群：
https://jq.qq.com/?_wv=1027&k=4BwGEoV
