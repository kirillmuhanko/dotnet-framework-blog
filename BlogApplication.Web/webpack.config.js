const CssMinimizerPlugin = require('css-minimizer-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const UglifyJsPlugin = require("uglifyjs-webpack-plugin");
const path = require('path');
const webpack = require('webpack');

function srcPath(subDir) {
    return path.join(__dirname, "Scripts", subDir);
}

module.exports = {
    entry: './Scripts/index.ts',
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
            },
            {
                test: /\.css$/,
                use: [MiniCssExtractPlugin.loader, 'css-loader']
            },
            {
                test: /\.(svg|eot|woff|woff2|ttf)$/,
                use: ['file-loader?name=[name].bundle.[ext]&outputPath=fonts']
            }
        ],
    },
    optimization: {
        minimizer: [
            new CssMinimizerPlugin(),
            new UglifyJsPlugin()
        ],
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: "css/blog.bundle.css"
        }),
        new webpack.DefinePlugin({
            'NODE_ENV': JSON.stringify(process.env.NODE_ENV)
        }),
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery',
            ko: 'knockout'
        }),
        new webpack.SourceMapDevToolPlugin({
            exclude: ['bootstrap.min.css']
        })
    ],
    resolve: {
        alias: {
            ajax: srcPath('app/ajax'),
            components: srcPath('app/components'),
            constants: srcPath('app/constants'),
            models: srcPath('app/models'),
            pages: srcPath('app/pages'),
            shared: srcPath('app/shared')
        },
        extensions: ['.js', '.css', '.ts', '.tsx' ]
    },
    output: {
        filename: 'js/blog.bundle.js',
        library: 'blogApp',
        libraryTarget: 'var',
        path: path.resolve(__dirname, 'wwwroot/bundles'),
        publicPath: '/bundles/'
    }
};
