import '../Styles/index.css';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';

export * from './app/pages';
export * from 'shared/declarations';
export * from 'shared/extensions';

if (process.env.NODE_ENV === 'development') {
    window.blogApp = this;
    window.jQuery = jQuery;
    window['$'] = jQuery;
}