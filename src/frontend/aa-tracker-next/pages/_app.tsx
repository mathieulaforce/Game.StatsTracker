import '../styles/globals.css';
import type { AppProps } from 'next/app';
import SideNavigationTemplate from '../components/templates/pageLayouts/navbarPageTemplate';

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <SideNavigationTemplate>
      <Component {...pageProps} />
    </SideNavigationTemplate>
  );
}

export default MyApp;
