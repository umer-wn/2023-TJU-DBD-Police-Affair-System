module.exports = {
    devServer: {
      proxy: {
        '/api': {
          target: 'https://localhost:7078',
          changeOrigin: true,
          pathRewrite: {
            '^/api': ''
          },
          secure: false  // 禁用 HTTPS 验证
        }
      }
    }
  };
  