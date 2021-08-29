# ThisNetWorks.OrchardCore.GoogleMaps

Google Maps Module for Orchard Core

This provides an extensible Google Maps module for Orchard Core

It uses the Google Maps javascript API, and Places features.

## Getting Started

Add reference to ThisNetWorks.OrchardCore.GoogleMaps module to your Orchard Core Startup Project

Enable the feature `Configuration -> Features -> Google Maps`

Configure your Api Key in `Configuration -> Settings -> Google Maps`

Add the `GoogleMapPart` to any Content Item you wish to display on a Google Map.

Currently built of the OrchardCore MyGet dev feed.

## Samples

The sample in `./samples` is based of the blog recipe, and has some overridden views to display the map.

To try the sample, clone the repo, and run the sample.

The sample has a custom setup event handler which will add the `GoogleMapPart` automatically
to the `BlogPost` and configure some defaults.

It also restricts the recipes available to only that of `TheBlogTheme`.

After running setup, setup your Api Key in `Configuration -> Settings -> Google Maps`

The initial blog post will have a default location, but to add multiple markers, 
clone a few `BlogPosts`, edit them to select a map location, and two maps will be available

1. Map with single marker on the `Detail` view of the `BlogPost`.
2. Map with multiple markers on the `Blog` page.

## Shapes and Queries

The module includes basic shapes seperating out the Google Maps Javascript API calls, and display.
You can override these to customise the javascript, and display the map how you prefer.

### Single Map View
- `GoogleMapPart` Used by the `DetailView` to serialize map data for a single item view.
- `GoogleMapPartContainer` Contains the div that the map will mount on. Override to adjust height / width etc.
- `GoogleMapPartMapInit` Google Map init code.
- `GoogleMapPartMarker` Google Map marker code. Override to display info window.

### Multiple Map View

- `GoogleMapPart_Summary` Summary view which serializes the map data for `DisplayMaps`
- `QueryGoogleMaps` Queries the part index and serializes the results, for `DisplayMaps`
- `DisplayMaps` Displays a Google Map with all data from a query, or summary display. No overrides for markers, just override the entire shape and render as required.
- `DisplayMapsContainer` Contains the div that the map will mount on. Override to adjust height / width etc.

## Versions

Version tags and pre release suffixes are based of the version of Orchard Core referenced.

For version 1.0 of Orchard Core use `1.0.0`, which will use the [Orchard Core NuGet Feed](https://www.nuget.org/packages/OrchardCore/).

Prerelease versions are suffixed with the CloudSmith build of Orchard Core referenced, 
and will required a configured CloudSmith NuGet feed. Refer [Configuring a preview package source](https://docs.orchardcore.net/en/latest/docs/getting-started/preview-package-source/)

e.g. `1.1.0-preview-16439` refers to the CloudSmith Orchard Core prerelease build `v1.1.0-preview-16439`

