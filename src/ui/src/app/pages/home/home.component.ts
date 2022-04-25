import { Component, ElementRef, HostListener, OnInit, ViewChild } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { LeafletControlLayersConfig, LeafletDirective } from '@asymmetrik/ngx-leaflet';
import * as L from 'leaflet';
import { catchError, delay, interval, of, switchMap, tap, timer } from 'rxjs';
import { GetFleetsRequest, GetAssetCategoriesRequest } from 'src/api/models';
import { AssetsService, AssetCategoriesService, FleetsService, FilesService } from 'src/api/services';

import { Uppy } from '@uppy/core';
import { DashboardOptions } from '@uppy/dashboard';
import Tus from '@uppy/tus';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

    @ViewChild('searchFilterField') searchInput!: ElementRef<HTMLInputElement>;
    @ViewChild(LeafletDirective, { static: true }) leafletDirective!: LeafletDirective;

    constructor(private assetsService: AssetsService,
        private assetCategoriesService: AssetCategoriesService,
        private fleetsService: FleetsService,
        private filesService: FilesService,
        private activatedRoute: ActivatedRoute) { }

    pipe = new DatePipe('en-US');

    tusEndpoint: string = 'https://localhost:5001/uploads/';
    filesEndpoint: string = this.filesService.rootUrl + '/api/files/download/';

    uppy: Uppy = new Uppy({
        debug: true,
        autoProceed: true,
        restrictions: {
            allowedFileTypes: ['.csv']
        },
        onBeforeUpload: (files) => {
            for (const key in files) {
                const file = files[key];
                if (!file.progress || !file.progress['uploadComplete']) {
                    file['meta']['size'] = file['meta'] || {};
                    file['meta']['size'] = file['size'];
                    file['meta']['date'] = this.pipe.transform(Date.now(), 'yyyy-MM-ddTHH:mm:ss');
                }
            }

            return files;
        }
    });

    uppyDashboardOptions: DashboardOptions = {
        proudlyDisplayPoweredByUppy: false,
        showRemoveButtonAfterComplete: false,
        hideCancelButton: true,
        doneButtonHandler: undefined,
        fileManagerSelectionType: 'files',
        note: 'Only csv files are allowed',
        showLinkToFileUploadResult: true,
        disablePageScrollWhenModalOpen: true,
        closeModalOnClickOutside: true,
        closeAfterFinish: true,
        animateOpenClose: true
    };

    uppyDashboardModalOpen: boolean = false;

    options: L.MapOptions = {
        layers: [
            L.tileLayer(
                'https://tiles.stadiamaps.com/tiles/alidade_smooth/{z}/{x}/{y}{r}.png', {
                maxZoom: 20, attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            })
        ],
        zoom: 14,
        zoomControl: false,
        center: L.latLng(14.594777, 121.054463)
    };

    layersControl: LeafletControlLayersConfig = {
        baseLayers: {
            'Default': L.tileLayer('https://tiles.stadiamaps.com/tiles/alidade_smooth/{z}/{x}/{y}{r}.png', { maxZoom: 20 }),
            'CH Swisstopo LBM Dark': L.tileLayer('https://api.maptiler.com/maps/ch-swisstopo-lbm-dark/{z}/{x}/{y}.png?key=dKFpUJ6juOZ4bJH0Cl6y', { maxZoom: 18 }),
            'Basic': L.tileLayer('https://api.maptiler.com/maps/bright/{z}/{x}/{y}.png?key=dKFpUJ6juOZ4bJH0Cl6y', { maxZoom: 18 })
        },
        overlays: {

        }
    }

    fleets: any = [];
    assetCategories: any = [];
    files: any = [];
    layers: L.Layer[] = [];
    searchFilter!: string;

    activeFleet: number | undefined = undefined;
    activeAssetCategory: number | undefined = undefined;
    activeFile: string | undefined = undefined;

    assetsLoading: boolean = false;
    assetCategoriesLoading: boolean = false;
    fleetsLoading: boolean = false;
    filesLoading: boolean = false;

    ngOnInit(): void {
        this.activatedRoute.queryParams.subscribe(params => {
            let fleetId = params['fleet'];
            if (fleetId == undefined) this.activeFleet = undefined;
            else this.activeFleet = parseInt(fleetId);

            let assetCategoryId = params['assetCategory'];
            if (assetCategoryId == undefined) this.activeAssetCategory = undefined;
            else this.activeAssetCategory = parseInt(assetCategoryId);

            this.activeFile = params['file'];

            this.loadAssets();
        });

        this.uppy.use(Tus, {
            endpoint: this.tusEndpoint,
            chunkSize: 30000000, // (~28.6 MB) which is default max request body size for IIS and kestrel
            removeFingerprintOnSuccess: true
        });

        this.uppy.on('complete', (result) => {
            for (const key in result.successful) {
                const file = result.successful[key] as any;
                if (file) {
                    this.uppy.setFileState(file.id, {
                        uploadURL: file.uploadURL?.replace(this.tusEndpoint, this.filesEndpoint + "?FileId=")
                    });
                }
            }

            this.loadFilters();
        });

        this.loadFilters();
    }

    loadFilters() {
        let getAssetsCategoriesRequery: GetAssetCategoriesRequest = {};
        this.assetCategoriesLoading = true;
        this.assetCategoriesService.apiAssetcategoriesGet$Json({ request: getAssetsCategoriesRequery })
            .pipe(delay(1000))
            .subscribe({
                next: (response) => {
                    if (response.assetCategories == null) return;

                    this.assetCategories = response.assetCategories;
                },
                error: (response) => {
                    this.assetCategoriesLoading = false;
                },
                complete: () => {
                    this.assetCategoriesLoading = false;
                }
            });

        let getFleetsRequest: GetFleetsRequest = {};
        this.fleetsLoading = true;
        this.fleetsService.apiFleetsGet$Json({ request: getFleetsRequest })
            .pipe(delay(1000))
            .subscribe({
                next: (response) => {
                    if (response.fleets == null) return;

                    this.fleets = response.fleets;
                },
                error: (response) => {
                    this.fleetsLoading = false;
                },
                complete: () => {
                    this.fleetsLoading = false;
                }
            });

        this.filesLoading = true;
        this.filesService.apiFilesGet$Json()
            .pipe(delay(1000))
            .subscribe({
                next: (response) => {
                    if (response.files == null) return;

                    this.files = response.files;
                },
                error: (response) => {
                    this.filesLoading = false;
                },
                complete: () => {
                    this.filesLoading = false;
                }
            });
    }

    loadAssets() {
        this.assetsLoading = true;
        timer(0, 3000)
            .pipe(
                delay(1000),
                switchMap(() => this.assetsService.apiAssetsGet$Json({ FleetId: this.activeFleet, AssetCategoryId: this.activeAssetCategory, FileId: this.activeFile })),
                tap(response => {
                    this.layers = this.layers.filter(l => false);
                    this.assetsLoading = false;
                    if (response.assets == null) return;

                    let assets = response.assets.filter(a => a.lastKnownLocation != null);
                    let markers = assets.map(a => {
                        let latLng = L.latLng(a.lastKnownLocation!!.latitude!!, a.lastKnownLocation!!.longitude!!);
                        let marker = L.marker(latLng, {
                            icon: L.icon({
                                iconUrl: a.assetCategory?.iconPath || 'http://www.clker.com/cliparts/X/4/d/k/m/q/map-pin.svg.hi.png',
                                iconSize: [48, 48],
                            }),
                            title: a.name!!
                        });

                        marker.bindPopup(`<strong><center>${a.name}</br>${a.assetCategory?.name}</center></strong>`, { closeButton: false, className: 'text-center' });

                        marker.on('mouseover', (e) => {
                            e.target.openPopup();
                        });

                        return marker;
                    });

                    markers.forEach(m => this.layers.push(m));
                }),
                catchError(response => {
                    this.assetsLoading = false;
                    return of(response);
                })
            ).subscribe();
    }

    get filteredLayers(): L.Layer[] {
        if (this.searchFilter == null) return this.layers;

        return this.layers.filter(l => {
            if (l instanceof L.Marker) {
                let marker: L.Marker = l;
                return marker.options.title?.toLowerCase().includes(this.searchFilter.toLowerCase());
            }

            return false;
        });
    }

    @HostListener('window:keydown', ['$event'])
    onKeyDown($event: KeyboardEvent): void {
        if ($event.getModifierState && $event.getModifierState('Control') && $event.keyCode === 70) {
            $event.preventDefault();
            this.searchInput.nativeElement.focus();
        }
    }
}
