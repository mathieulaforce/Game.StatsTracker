import * as React from 'react';
import StickyNavbar from '../../ui/organism/navbar/stickyNavbar';

const NavbarPageTemplate: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  return (
    <div className="antialiased text-slate-400  bg-slate-900 h-full">
      <div className="absolute z-20 top-0 inset-x-0 flex justify-center overflow-hidden pointer-events-none">
        <div className="w-[108rem] flex-none flex justify-end">
          <picture>
            <source srcSet="/background/dark_small.avif" type="image/avif" />
            <img src="/background/dark_small.png" alt="" className="w-[90rem] flex-none max-w-none" />
          </picture>
        </div>
      </div>
      <div className="flex flex-col gap-4 h-full overflow-auto">
        <StickyNavbar />
        <main className="flex-grow w-full px-8">{children}</main>
      </div>
    </div>
  );
};

export default NavbarPageTemplate;
