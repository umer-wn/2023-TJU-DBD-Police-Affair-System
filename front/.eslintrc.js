// https://eslint.org/docs/user-guide/configuring

module.exports = {
  root: true,
  parserOptions: {
    parser: 'babel-eslint'
  },
  env: {
    browser: true,
  },
  extends: [
    // https://github.com/vuejs/eslint-plugin-vue#priority-a-essential-error-prevention
    // consider switching to `plugin:vue/strongly-recommended` or `plugin:vue/recommended` for stricter rules.
    'plugin:vue/essential',
    // https://github.com/standard/standard/blob/master/docs/RULES-en.md
    'standard'
  ],
  // required to lint *.vue files
  plugins: [
    'vue'
  ],
  // add your custom rules here
  rules: {
    // allow async-await
    'generator-star-spacing': 'off',
    // allow debugger during development
    'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    // 函数定义时括号前面要不要有空格
    "space-before-function-paren": [0, "always"],
    'no-console': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    // 连续空格问题
    'no-multi-spaces': 'off',
    // 分号问题
    'semi': 'off',
    // 行尾结束符问题
    'eol-last': 'off',
    // 逗号问题
    'comma-dangle': 'off',
    // 花括号风格问题
    'brace-style': 'off',
    // 引号风格问题
    'quotes': 'off',
    // 返回空值问题
    "no-useless-return": "off",
    // 使用制表符的问题
    'no-tabs': 'off',
    // 制表符与空格混用问题
    'no-mixed-spaces-and-tabs': 'off',
    // 未使用变量的问题
    'no-unused-vars': 'off',
    // 多个空行
    "no-multiple-empty-lines": "off",
  }
}
