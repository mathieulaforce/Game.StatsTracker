import '../styles/globals.css';
import type { AppProps } from 'next/app';
import SideNavigationTemplate from '../components/templates/pageLayouts/navbarPageTemplate';

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <div className="bg-slate-900 h-full">
      <SideNavigationTemplate>
        <Component {...pageProps} />
      </SideNavigationTemplate>
    </div>
  );
}

export default MyApp;
