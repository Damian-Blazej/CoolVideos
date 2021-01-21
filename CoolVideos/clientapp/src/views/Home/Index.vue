<template>
    <div>
        <div class="bg-light text-primary py-5">
            <b-container>
                <b-row>
                    <b-col md="12">
                        <div class="text-left">
                            <h1 class="font-weight-bold">Siema na CoolVideos!  💪 </h1>
                            <h2>Obejrzysz tutaj HIT filmy 🎬</h2> 
                            <h2>Dołącz do nas całkowicie za darmo! 🤑</h2>
                            <b-button id="loginButton" v-if="!isLogged" squared variant="primary" size="lg" :to="{ name: 'Login' }" class="mt-4 px-5">
                                Dołącz!
                                <i class="bi bi-door-open"></i>
                            </b-button>
                        </div>
                    </b-col>
                </b-row>
            </b-container>
        </div>
        <div class="mt-5">
            <b-container>
                <div>
                    <!-- <div class="text-center">
                        <h2 class="font-weight-bold">Osiągnij z nami sukces</h2>
                        <b-row class="mt-4 d-flex justify-content-center">
                            <b-col md="4" class="my-3 w-75">
                                <img src="../../assets/icons/book.png" class="img-fluid mb-2" style="max-width: 100px;" />
                                <h5>Ucz się najnowszych technologii, takich jak Python, JavaScipt, czy Machine Learning.</h5>
                            </b-col>
                            <b-col md="4" class="my-3 w-75">
                                <img src="../../assets/icons/tie.png" class="img-fluid mb-2" style="max-width: 100px;" />
                                <h5>Czerp wiedzę od ekspertów w swojej dziedzinie.</h5>
                            </b-col>
                            <b-col md="4" class="my-3 w-75">
                                <img src="../../assets/icons/id-card.png" class="img-fluid mb-2" style="max-width: 100px;" />
                                <h5>Dziel się swoją wiedzą z innymi.</h5>
                            </b-col>
                            
                        </b-row>
                    </div> -->
                </div>
                <div class="mt-3 text-center">
                    <h2 class="font-weight-bold">Najnowsze filmy</h2>
                    <div class="d-flex justify-content-center align-items-center flex-wrap mt-4">
                        <VideoCard v-for="video in videos" :key="video.videoId"
                                   :video-id="video.id"
                                   :title="video.title"
                                   :img-src="`${$serverUrl}/Resources/Images/${video.image}`"
                                   :img-alt="video.title + ' image'"
                                   :author="video.user.firstName + ' ' + video.user.lastName">
                        </VideoCard>
                    </div>
                </div>
            </b-container>
        </div>
    </div>
</template>

<script>
    import VideoCard from "../../components/VideoCard";
    import { mapGetters } from 'vuex'

    export default {
        name: 'Home',
        components: {
            VideoCard,
        },
        computed: mapGetters([
            'isLogged'
        ]),
        data() {
            return {
                videos: null
            }          
        },
        methods: {
            fetchLatestVideos() {
                this.$http.get(this.$serverUrl + '/api/video/latest?n=3')
                    .then(resoult => { this.videos = resoult.data;})
            }
        },
        mounted() {
            this.fetchLatestVideos();
        }
    }
</script>
