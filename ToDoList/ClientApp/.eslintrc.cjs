const prettierConfig = {
    bracketSameLine: false,
    printWidth: 80,
    singleQuote: false,
    useTabs: false,
    tabWidth: 4,
    endOfLine: "lf",
    semi: false,
}

module.exports = {
    "env": {
        "browser": true,
        "es2021": true,
        "node": true
    },
    "plugins": [
        "react",
        "functional",
        "@typescript-eslint"
    ],
    "extends": [
        "eslint:recommended",
        "plugin:react/recommended",
        "plugin:@typescript-eslint/eslint-recommended",
        "plugin:@typescript-eslint/strict",
        "plugin:@typescript-eslint/recommended-requiring-type-checking",
        "plugin:functional/external-vanilla-recommended",
        "plugin:functional/recommended",
        "plugin:functional/stylistic",
        "plugin:prettier/recommended",
    ],
    "settings": {
        "react": {
            "version": "detect"
        }
    },
    "overrides": [
    ],
    "parser": "@typescript-eslint/parser",
    "parserOptions": {
        "ecmaVersion": "latest",
        "sourceType": "module",
        "project": "./tsconfig.json"
    },
    "rules": {
        "indent": [
            "error",
            4
        ],
        "linebreak-style": [
            "error",
            "unix"
        ],
        "quotes": [
            "error",
            "double"
        ],
        "semi": [
            "error",
            "never"
        ],
        "no-restricted-syntax": [
            "error",
            {
                "selector": "ClassDeclaration",
                "message": "Classes are not allowed"
            },
            "warning",
            {
                "selector": "FunctionExpression",
                "message": "Function expressions are not allowed."
            },
            {
                "selector": "CallExpression[callee.name='setTimeout'][arguments.length!=2]",
                "message": "setTimeout must always be invoked with two arguments."
            }
        ],
        "react/react-in-jsx-scope": "off",
        "react/prop-types": "error",
        "object-curly-spacing": ["error", "always"],
        "react/jsx-filename-extension": [1, { "extensions": [".tsx"] }],

        // @typescript-eslint
        "@typescript-eslint/consistent-type-definitions": ["error", "type"],
        // Disabling JS no-unused-vars to allow TS function types, re-enabling it for TS
        "no-unused-vars": "off",
        "@typescript-eslint/no-unused-vars": [
            "error",
            {
                "argsIgnorePattern": "^_",
                "varsIgnorePattern": "^_",
                "caughtErrorsIgnorePattern": "^_"
            }
        ],

        // eslint-plugin-functional
        "functional/readonly-type": ["error", "keyword"],
        // Impossible with react setState
        "functional/no-return-void": "off",
        "functional/no-expression-statements": "off",
        "functional/no-conditional-statements": "off",
        // Reason about how to enable this one
        "functional/prefer-immutable-types": "off",
        "functional/functional-parameters": ["off", {
            "enforceParameterCount": {
                "ignoreLambdaExpression": true,
                "ignoreIIFE": true
            },
        }],

        // prettier
        "prettier/prettier": [
            "error",
            prettierConfig
        ]
    }
}