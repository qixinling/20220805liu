module.exports = {
	// 转app时需要./才不会显示空白
	publicPath: process.env.NODE_ENV === 'production' ?
		'./' : '/',
	outputDir: 'dist', //打包后的输出目录
	lintOnSave: true, //保存时是不是用eslint-loader 来lint 代码
	runtimeCompiler: true, //关键点在这
	productionSourceMap: true, //打包时是否生成.map文件
	// 调整内部的 webpack 配置。
	// 查阅 https://github.com/vuejs/vue-doc-zh-cn/vue-cli/webpack.md
	chainWebpack: () => {},
	configureWebpack: () => {},
	// 配置 webpack-dev-server 行为。
	devServer: {
		open: process.platform === 'darwin',
		hot: true, //自动保存
		open: true, //自动启动
		host: '0.0.0.0',
		port: 8080,
		https: false,
		hotOnly: false,
		// 查阅 https://github.com/vuejs/vue-doc-zh-cn/vue-cli/cli-service.md#配置代理
		proxy: null, // string | Object
		before: app => {}
	}
}
