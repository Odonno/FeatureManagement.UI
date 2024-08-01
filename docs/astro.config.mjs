import { defineConfig } from "astro/config";
import starlight from "@astrojs/starlight";

// https://astro.build/config
export default defineConfig({
  integrations: [
    starlight({
      title: "FeatureManagement",
      social: {
        github: "https://github.com/Odonno/FeatureManagement.UI",
      },
      sidebar: [
        {
          label: "Introduction",
          autogenerate: { directory: "introduction" },
        },
        {
          label: "Data storage",
          autogenerate: { directory: "storage" },
        },
        {
          label: "Feature types",
          autogenerate: { directory: "features" },
        },
        {
          label: "User Interface",
          autogenerate: { directory: "ui" },
        },
        {
          label: "Feature consumption",
          autogenerate: { directory: "api" },
        },
        {
          label: "Reference",
          autogenerate: { directory: "reference" },
        },
      ],
    }),
  ],
});
