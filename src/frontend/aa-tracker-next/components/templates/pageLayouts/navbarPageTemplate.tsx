import * as React from 'react';
import Navbar from '../../ui/organism/navbar/navbar';
import StickyNavbar from '../../ui/organism/navbar/stickyNavbar';

const NavbarPageTemplate: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <div className="antialiased  text-slate-400  bg-slate-900 vw">
      <div className="absolute z-20 top-0 inset-x-0 flex justify-center overflow-hidden pointer-events-none">
        <div className="w-[108rem] flex-none flex justify-end">
          <picture>
            <source srcSet="/background/dark_small.avif" type="image/avif" />
            <img src="/background/dark_small.png" alt="" className="w-[90rem] flex-none max-w-none" />
          </picture>
        </div>
      </div>
      <StickyNavbar />
      {/* <Navbar /> */}
      <main className="hidden lg:block fixed z-20 inset-0 top-[5rem] pb-10 px-8 overflow-y-auto">{children}</main>
    </div>
  );
};

export default NavbarPageTemplate;
